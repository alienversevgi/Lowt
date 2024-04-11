using GamePlay.Characters.Enemys.RangedHuman;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanState : State
    {
        protected RangedHumanController _controller;
        protected RangedHumanView _view => _controller.View;
        protected RangedHumanData _data => _controller.Data;
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