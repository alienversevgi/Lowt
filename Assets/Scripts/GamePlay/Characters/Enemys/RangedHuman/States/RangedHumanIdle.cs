using GamePlay.Components;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanIdle : RangedHumanState
    {
        [SerializeField] private SightArea sightArea;
        
        public override void Enter()
        {
            sightArea.OnEnterToSight.AddListener(PlayerEnterToSight);
            sightArea.enabled = true;
            _view.AnimationHandler.Play(RangedHumanStateType.Idle);
        }

        private void PlayerEnterToSight(GameObject playerObject)
        {
            _controller.Target = playerObject.transform;
            _controller.StateController.ChangeState(nameof(RangedHumanFlee));
        }

        public override void Exit()
        {
            sightArea.enabled = false;
            sightArea.OnEnterToSight.RemoveListener(PlayerEnterToSight);
        }
    }
}