namespace GamePlay.Characters.Enemys
{
    public class RangedHumanState : State
    {
        protected RangedHumanController _controller;
        
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