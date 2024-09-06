using System;
using Cysharp.Threading.Tasks;
using GamePlay.Components;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class ZombieMoveState : ZombieState
    {
        [SerializeField] private SightArea sightArea;

        public override void Enter()
        {
            base.Enter();
            _view.AnimationHandler.PlayMove();
            StartSightControl().Forget();
            _controller.Move(_controller.DestructableTarget.position, () => _stateController.ChangeState(nameof(ZombieDestructState)));
        }

        private async UniTask StartSightControl()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_data.StartSearchTime));
            sightArea.SetEnable(true);
            sightArea.OnEnterToSight.AddListener(OnEnterToSight);
        }

        private void OnEnterToSight(GameObject enteredObject)
        {
            _controller.DamagableTarget = enteredObject.transform;
            _stateController.ChangeState(nameof(ZombieChaseState));
        }

        public override void Exit()
        {
            sightArea.SetEnable(false);
            _controller.Stop();
            base.Exit();
        }
    }
}