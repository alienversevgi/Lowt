using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class ZombieMoveState : ZombieState
    {
        public override void Enter()
        {
            base.Enter();
            _view.AnimationHandler.Play(ZombieStateType.Run);
            _controller.Move(Vector3.zero, OnMoveCompleted);
        }

        private void OnMoveCompleted()
        {
            _stateController.ChangeState(nameof(ZombieAttackState));
        }
    }
}