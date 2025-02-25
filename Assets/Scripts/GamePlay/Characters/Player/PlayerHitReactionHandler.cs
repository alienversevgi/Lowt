using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Weapons;
using UnityEngine;

namespace GamePlay.Characters
{
    public class PlayerHitReactionHandler : MonoBehaviour
    {
        [SerializeField] private float groundWaitDuration;
        
        public bool IsExecuting { get; private set; }
        private PlayerController _player;

        public void Initialize(PlayerController player)
        {
            _player = player;
        }
        
        public async UniTask Execute(DamageData data)
        {
            IsExecuting = true;
            var diff = this.transform.position - data.Owner.transform.position;
            var point = this.transform.position - diff.normalized * -2;
            
            this.transform.DOLookAt(data.Owner.transform.position, .2f, AxisConstraint.Y, Vector3.up);
            this.transform.DOPunchScale(Vector3.one * .5f, .2f, 5, 0); 
            _player.View.AnimationHandler.Play(PlayerAnimationState.Death);
            
            await this.transform.DOMove(point, 0.3f);
            
            await UniTask.Delay(TimeSpan.FromSeconds(groundWaitDuration));
            
            _player.View.AnimationHandler.Play(PlayerAnimationState.GettingUp);
            float duration = _player.View.AnimationHandler.GetCurrentAnimationLength();
            await UniTask.Delay(TimeSpan.FromSeconds(duration/4));
            IsExecuting = false;
        }
    }
}