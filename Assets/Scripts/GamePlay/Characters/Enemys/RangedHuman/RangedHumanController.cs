using System;
using GamePlay.Characters.Enemys;
using GamePlay.Characters.Enemys.RangedHuman;
using UnityEngine;

namespace GamePlay.Characters
{
    public class RangedHumanController : Enemy
    {
        public StateController StateController;
        public RangedHumanView View;
        public RangedHumanData Data;

        public Transform Target { get; set; }
        
        public override void Move(Vector3 point, Action callback)
        {
            View.AnimationHandler.Play(RangedHumanStateType.Run);
            base.Move(point, callback);
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                View.AnimationHandler.Play(RangedHumanStateType.Idle);

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                View.AnimationHandler.Play(RangedHumanStateType.Run);
            }
        }
    }
}