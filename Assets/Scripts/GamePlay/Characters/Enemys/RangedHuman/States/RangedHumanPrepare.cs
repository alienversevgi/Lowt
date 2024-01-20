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
            Debug.Log("RangedHumanPrepare enter");
            _view.AnimationHandler.Play(RangedHumanStateType.Prepare);
            WaitAndExecute(TimeSpan.FromSeconds(prepareDuration), OnPrepareCompleted).Forget();
        }

        private void OnPrepareCompleted()
        {
            Debug.Log("PrepareCompleted");
            _stateController.ChangeState(nameof(RangedHumanAttack));
        }

        private async UniTask WaitAndExecute(TimeSpan duration, Action callBack)
        {
            Debug.Log("RangedHumanPrepare delay enter");
            await UniTask.Delay(duration);
            Debug.Log("RangedHumanPrepare delay exit");
            callBack.Invoke();
        }
    }
}