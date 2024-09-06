using System;
using Cysharp.Threading.Tasks;
using GamePlay.Weapons;
using UnityEngine;
using Range = GamePlay.Components.Range;

namespace GamePlay.Characters.Enemys
{
    public class ZombieController : Enemy, IDamageable
    {
        [SerializeField] private Range range;

        public StateController StateController;
        public ZombieView View;
        public ZombieData Data;

        public Transform DamagableTarget;
        public Transform DestructableTarget;

        protected override void Initialize()
        {
            base.Initialize();
            Data = Instantiate(Data);
            SetSpeed(Data.Speed);
        }

        public override void Move(Vector3 point, Action callback)
        {
            View.AnimationHandler.Play(ZombieStateType.Run);
            base.Move(point, callback);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                View.AnimationHandler.Play(ZombieStateType.Idle);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                View.AnimationHandler.Play(ZombieStateType.Run);
            }
        }

        public void ApplyDamage(DamageData data)
        {
            Data.HP -= data.Amount;
            if (Data.HP <= 0)
            {
                StateController.ChangeState(nameof(ZombieDeadState));
            }
            else
            {
                View.PlayHitReaction().Forget();
                if (StateController.IsOnState(nameof(ZombieMoveState)))
                {
                    DamagableTarget = data.Owner.transform;
                    StateController.ChangeState(nameof(ZombieChaseState));
                }
            }
        }

        public async UniTask<bool> IsTargetOnRange()
        {
            return await range.IsOnRange(DamagableTarget.gameObject);
        }
    }
}