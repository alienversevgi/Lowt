namespace GamePlay.Character
{
    public class RangedHumanState : State
    {
        protected RangedHumanController _controller;
        protected RangedHumanView _view;
        
        public override void Initialize()
        {
            _controller = this.GetComponent<RangedHumanController>();
            _view = this.GetComponent<RangedHumanView>();
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