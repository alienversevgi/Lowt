namespace GamePlay.Characters.Enemys
{
    public class ZombieAttackState : ZombieState
    {
        public override void Enter()
        {
            base.Enter();
            _view.AnimationHandler.Play(ZombieStateType.Attack);
        }
    }
}