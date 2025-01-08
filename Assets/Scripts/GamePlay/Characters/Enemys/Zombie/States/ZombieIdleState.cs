namespace GamePlay.Characters.Enemys
{
    public class ZombieIdleState : ZombieState
    {
        public override void Enter()
        {
            base.Enter();
            _stateController.ChangeState(nameof(ZombieMoveState));
        }
    }
}