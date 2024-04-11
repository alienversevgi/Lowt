using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanAttack : RangedHumanState
    {
        public override void Enter()
        {
            base.Enter();
            AttackSequence().Forget();
        }

        private async UniTask AttackSequence()
        {
            var player = GameObject.FindObjectOfType<PlayerController>();
            _view.AnimationHandler.Play(RangedHumanStateType.Idle);
            await this.transform.DOLookAt(player.transform.position, .3f).SetEase(Ease.Linear);
            _view.AnimationHandler.Play(RangedHumanStateType.Attack);

            await UniTask.WaitWhile(() => _view.AnimationHandler.GetBool("IsAttackFrameReached") == false);

            var projectile = _view.GetProjectile();
            projectile.Shoot(player.transform.position - this.transform.position, _data.ProjectileForce);
            _controller.StateController.ChangeState(nameof(RangedHumanIdle));
        }
    }
}