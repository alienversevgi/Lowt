using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Components;
using GamePlay.Weapons;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class ZombieAttackState : ZombieState
    {
        [SerializeField] private Dash dash;

        public override void Enter()
        {
            base.Enter();
            dash.Initialize(Exit);
            RunAttackSequence().Forget();
        }

        private async UniTask RunAttackSequence()
        {
            _view.AnimationHandler.Play(ZombieStateType.Charge);
            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
            Dash();
            await Attack();
            if (!_data.IsDead)
                _stateController.ChangeState(nameof(ZombieChaseState));
            
            async Task Attack()
            {
                _view.AnimationHandler.Play(ZombieStateType.Attack);
                var duration = _view.AnimationHandler.AttackAnimationLength;
                await UniTask.Delay(TimeSpan.FromSeconds(duration * .2f));
                if (await _controller.IsTargetOnRange())
                    _controller.DamagableTarget.GetComponent<IDamageable>().ApplyDamage(new DamageData(_controller.gameObject,_data.EntityDamage));

                await UniTask.Delay(TimeSpan.FromSeconds(duration * .8f));
            }

            void Dash()
            {
                var targetPosition = _controller.DamagableTarget.position;
                this.transform.DOLookAt(targetPosition, .2f);
                var direction = targetPosition - this.transform.position;
                direction.Normalize();
                dash.Execute(direction, _data.AttackMoveUnit, _data.AttackMoveSpeed);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}