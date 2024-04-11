using System;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class ZombieController : Enemy
    {
        public StateController StateController;
        public ZombieView View;
        public ZombieData Data;

        public Transform Target;

        protected override void Initialize()
        {
            base.Initialize();
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
    }
}