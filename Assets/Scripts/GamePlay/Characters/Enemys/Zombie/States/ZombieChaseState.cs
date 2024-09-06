using GamePlay.Handler;

namespace GamePlay.Characters.Enemys
{
    public class ZombieChaseState : ZombieState
    {
        public override void Enter()
        {
            base.Enter();
            _controller.SetSpeed(_data.ChaseSpeed);
            _view.AnimationHandler.PlayRun();
            FollowHandler.Instance.Follow(new FollowerData(_controller,
                                                           _controller.DamagableTarget,
                                                           _data.AttackRange,
                                                           _data.ChaseIgnoreDuration,
                                                           ReachedToIgnoreDistance,
                                                           ReachedToIgnoreDuration
                                          )
            );
        }

        private void ReachedToIgnoreDistance()
        {
            _stateController.ChangeState(nameof(ZombieAttackState));
        }
        
        private void ReachedToIgnoreDuration()
        {
            _stateController.ChangeState(nameof(ZombieMoveState));
        }

        public override void Exit()
        {
            _controller.SetSpeed(_data.Speed);
            FollowHandler.Instance.Ignore(_controller);
            base.Exit();
        }
    }
}