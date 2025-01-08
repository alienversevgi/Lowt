using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using GamePlay.Weapons;

namespace GamePlay.Characters.Enemys
{
    public class ZombieHitReactionState : ZombieState
    {
        private DamageData _damageData;

        public override void Enter()
        {
            base.Enter();
            Test().Forget();
            // if (_data.HP <= 0)
            // {
            //     _stateController.ChangeState(nameof(ZombieDeadState));
            // }
            // else
            // {
            //     _view.PlayHitReaction().Forget();
            //     var diff = this.transform.position - _damageData.Owner.transform.position;
            //     var point = this.transform.position - diff.normalized * -2;
            //
            //     this.transform.DOMove(point, 0.5f).OnComplete(() =>
            //     {
            //         _stateController.ChangeState(nameof(ZombieChaseState));
            //     });
            // }
        }


        private async UniTask Test()
        {
            if (_data.HP <= 0)
            {
                _stateController.ChangeState(nameof(ZombieDeadState));
                return;
                
            }
            
            var diff = this.transform.position - _damageData.Owner.transform.position;
            var point = this.transform.position - diff.normalized * -2;
            _view.PlayHitReaction().Forget();
            await this.transform.DOMove(point, 0.3f);
            _stateController.ChangeState(nameof(ZombieChaseState));
        }

        public void SetDamageData(DamageData data)
        {
            _damageData = data;
        }
    }
}