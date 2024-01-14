namespace GamePlay.Characters.Enemys
{
    public class RangedHumanState : State
    {
        protected RangedHumanController _controller;
        protected RangedHumanView _view => _controller.View;
        protected StateController _stateController => _controller.StateController;
        
        public override void Initialize()
        {
            _controller = this.GetComponent<RangedHumanController>();
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