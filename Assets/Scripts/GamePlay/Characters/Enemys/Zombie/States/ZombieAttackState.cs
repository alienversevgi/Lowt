using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Components;
using GamePlay.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Characters.Enemys
{
    public class ZombieAttackState : ZombieState
    {
        [SerializeField] private Roll roll;
        private CancellationTokenSource _cancellationTokenSource;

        public override void Enter()
        {
            base.Enter();
            _cancellationTokenSource = new CancellationTokenSource();
            roll.Initialize(_data.AttackMoveUnit, _data.AttackMoveSpeed);
            roll.SetOnComplete(DetermineNextState);
            RunAttackSequence().Forget();
        }

        private async UniTask RunAttackSequence()
        {
            _view.AnimationHandler.Play(ZombieStateType.Charge);
            await UniTask.Delay(TimeSpan.FromSeconds(.5f), cancellationToken: _cancellationTokenSource.Token);
            Dash();
            await Attack();

            async Task Attack()
            {
                _view.AnimationHandler.Play(ZombieStateType.Attack);
                var duration = _view.AnimationHandler.AttackAnimationLength;
                await UniTask.Delay(TimeSpan.FromSeconds(duration * .2f),
                    cancellationToken: _cancellationTokenSource.Token);
                if (await _controller.IsTargetOnRange())
                    _controller.DamagableTarget.GetComponent<IDamageable>()
                        .ApplyDamage(new DamageData(_controller.gameObject, _data.EntityDamage));

                await UniTask.Delay(TimeSpan.FromSeconds(duration * .8f),
                    cancellationToken: _cancellationTokenSource.Token);
            }

            void Dash()
            {
                var targetPosition = _controller.DamagableTarget.position;
                this.transform.DOLookAt(targetPosition, .2f);
                var direction = targetPosition - this.transform.position;
                direction.Normalize();
                roll.Execute(direction);
            }
        }

        private void DetermineNextState()
        {
            if (_data.IsDead)
                return;
            
            _stateController.ChangeState(nameof(ZombieChaseState));
        }

        public override void Exit()
        {
            base.Exit();

            _cancellationTokenSource.Cancel();
            roll.Cancel();
        }
    }
}