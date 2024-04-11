using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Components;
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
            Attack().Forget();
        }

        private async UniTask Attack()
        {
            _view.AnimationHandler.Play(ZombieStateType.Charge);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            this.transform.DOLookAt(_controller.Target.position, .2f);
            var direction = _controller.Target.position - this.transform.position;
            direction.Normalize();
            dash.Execute(direction, _data.AttackMoveUnit, _data.AttackMoveSpeed);
            _view.AnimationHandler.Play(ZombieStateType.Attack);
            await UniTask.Delay(TimeSpan.FromSeconds(_view.AnimationHandler.GetCurrentAnimationLength()));
            _stateController.ChangeState(nameof(ZombieMoveState));
        }
    }
}