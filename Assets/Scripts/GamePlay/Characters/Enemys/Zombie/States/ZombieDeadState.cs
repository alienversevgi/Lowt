namespace GamePlay.Characters.Enemys
{
    public class ZombieDeadState : ZombieState
    {
        public override void Enter()
        {
            base.Enter();
            _view.AnimationHandler.Play(ZombieStateType.Dead);
        }
    }
}