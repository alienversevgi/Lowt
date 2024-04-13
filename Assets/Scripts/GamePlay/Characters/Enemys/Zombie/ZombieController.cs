using System;
using Cysharp.Threading.Tasks;
using GamePlay.Weapons;
using UnityEngine;
using Range = GamePlay.Components.Range;

namespace GamePlay.Characters.Enemys
{
    public class ZombieController : Enemy , IDamagable
    {
        [SerializeField] private Range range;
        
        public StateController StateController;
        public ZombieView View;
        public ZombieData Data;

        public Transform Target;
        
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

        public void ApplyDamage(int value)
        {
            Data.HP -= value;
            if (Data.HP <= 0)
            {
                StateController.ChangeState(nameof(ZombieDeadState));
            }
            else
            {
                View.PlayHitReaction().Forget();
            }
        }

        public async UniTask<bool> IsTargetDamageable()
        {
            return await range.IsOnRange(Target.gameObject);
        }
    }
}