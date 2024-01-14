using GamePlay.Components;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanIdle : RangedHumanState
    {
        [SerializeField] private SightOfHandler sightOfHandler;
        
        public override void Enter()
        {
            sightOfHandler.enabled = true;
            sightOfHandler.OnEnterToSight.AddListener(PlayerEnterToSight);
            _view.AnimationHandler.Play(RangedHumanStateType.Idle);
        }

        private void PlayerEnterToSight(GameObject playerObject)
        {
            _controller.Target = playerObject.transform;
            _controller.StateController.ChangeState(nameof(RangedHumanFlee));
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
            sightOfHandler.enabled = false;
        }
    }
}