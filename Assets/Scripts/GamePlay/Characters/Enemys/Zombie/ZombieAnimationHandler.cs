using DG.Tweening;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class ZombieAnimationHandler : AnimationHandler
    {
        private readonly int _speedKey = Animator.StringToHash("Speed");
        private float _speedValue => animator.GetFloat(_speedKey);

        public float AttackAnimationLength
        {
            get
            {
                if (_attackAnimationLength == 0)
                {
                    _attackAnimationLength = GetAnimationClip(nameof(ZombieStateType.Attack)).length;
                }

                return _attackAnimationLength;
            }
        }

        private float _attackAnimationLength;

        public void Play(ZombieStateType stateType)
        {
            Play((int) stateType);
        }

        public void PlayMove()
        {
            Play(ZombieStateType.Run);
            DOVirtual.Float(_speedValue, 0, 1, (amount) => { animator.SetFloat(_speedKey, amount); });
        }

        public void PlayRun()
        {
            Play(ZombieStateType.Run);
            DOVirtual.Float(_speedValue, 1, 1, (amount) => { animator.SetFloat(_speedKey, amount); });
        }
    }
}