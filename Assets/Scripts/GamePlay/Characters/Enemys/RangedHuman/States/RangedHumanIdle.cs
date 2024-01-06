using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanIdle : RangedHumanState
    {
        public override void Enter()
        {
           
        }

        public override void Tick()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _controller.StateController.ChangeState(nameof(RangedHumanFlee));
            }
        }

        public override void Exit()
        {
          
        }
    }
}