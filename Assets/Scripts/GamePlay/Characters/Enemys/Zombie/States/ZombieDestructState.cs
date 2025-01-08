using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Building;

namespace GamePlay.Characters.Enemys
{
    public class ZombieDestructState : ZombieState
    {
        private CancellationTokenSource _cancellationToken;

        public override void Enter()
        {
            base.Enter();

            _cancellationToken = new CancellationTokenSource();
            RunDestructSequence().Forget();
        }

        private async UniTask RunDestructSequence()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                _view.AnimationHandler.Play(ZombieStateType.Idle);
                await UniTask.Delay(TimeSpan.FromSeconds(.5f), cancellationToken: _cancellationToken.Token);
                await Destruct();
                await UniTask.Delay(TimeSpan.FromSeconds(_data.DestructRate));
            }
        }

        private async UniTask Destruct()
        {
            _view.AnimationHandler.Play(ZombieStateType.Attack);
            var duration = _view.AnimationHandler.AttackAnimationLength;
            await UniTask.Delay(TimeSpan.FromSeconds(duration * .2f), cancellationToken: _cancellationToken.Token);
            var target = _controller.DestructableTarget.GetComponent<IDestructable>();
            target.Destruct(_data.BuildingDamage);

            await UniTask.Delay(TimeSpan.FromSeconds(duration * .8f), cancellationToken: _cancellationToken.Token);
        }

        public override void Exit()
        {
            base.Exit();
            _cancellationToken.Cancel();
        }
    }
}