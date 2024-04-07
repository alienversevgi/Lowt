namespace GamePlay.Characters.Enemys
{
    public class ZombieState : State
    {
        protected ZombieController _controller;
        protected ZombieView _view => _controller.View;
        protected StateController _stateController => _controller.StateController;
        
        public override void Initialize()
        {
            _controller = this.GetComponent<ZombieController>();
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}