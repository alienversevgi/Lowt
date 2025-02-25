using GamePlay.Extentions;
using GamePlay.Handler;

namespace GamePlay.Characters.Enemys
{
    public class ZombieChaseState : ZombieState
    {
        private FollowerData _followerData;

        public override void Initialize()
        {
            base.Initialize();

            _followerData = new FollowerData(_controller,
                    _controller.DamagableTarget,
                    _data.AttackRange,
                    _data.ChaseIgnoreDuration
                )
                .OnMoveStarted(OnChaseStarted)
                .OnReachedToIgnoreDistance(ReachedToIgnoreDistance)
                .OnReachedToIgnoreDuration(ReachedToIgnoreDuration);
        }

        public override void Enter()
        {
            base.Enter();
            _controller.SetSpeed(_data.ChaseSpeed);
            _followerData.SetTarget(_controller.DamagableTarget);

            FollowHandler.Instance.Follow(_followerData);
        }

        private void OnChaseStarted()
        {
            _view.AnimationHandler.PlayRun();
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