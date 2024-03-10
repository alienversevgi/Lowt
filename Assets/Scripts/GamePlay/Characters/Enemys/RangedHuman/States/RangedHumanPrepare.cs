using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanPrepare : RangedHumanState
    {
        [SerializeField] private float prepareDuration = .5f;

        public override void Enter()
        {
            base.Enter();

            _view.AnimationHandler.Play(RangedHumanStateType.Prepare);
            WaitAndExecute(TimeSpan.FromSeconds(prepareDuration), OnPrepareCompleted).Forget();
        }

        private void OnPrepareCompleted()
        {
            _stateController.ChangeState(nameof(RangedHumanAttack));
        }

        private async UniTask WaitAndExecute(TimeSpan duration, Action callBack)
        {
            await UniTask.Delay(duration);
            callBack.Invoke();
        }
    }
}