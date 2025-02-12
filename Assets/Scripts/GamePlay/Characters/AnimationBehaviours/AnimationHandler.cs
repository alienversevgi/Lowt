using System;
using UnityEngine;

namespace GamePlay.Characters
{
    [RequireComponent(typeof(Animator))]
    public class AnimationHandler : MonoBehaviour
    {
        private readonly int _state = Animator.StringToHash("State");
        
        protected Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayTrigger(int id)
        {
            _animator.SetTrigger(id);
        }

        public void Play(int state)
        {
            _animator.SetInteger(_state, state);
        }

        public void Play(int state, float speed = 1)
        {
            _animator.speed = speed;
            _animator.SetInteger(_state, state);
        }

        public void Play(int id, bool isOn)
        {
            _animator.SetBool(id, isOn);
        }

        public bool GetBool(string key)
        {
            return _animator.GetBool(key);
        }

        public float GetCurrentAnimationLength()
        {
            var clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            return clipInfo[0].clip.length;
        }

        public AnimationClip GetAnimationClip(string clipName)
        {
            AnimationClip result = null;
            var clips = _animator.runtimeAnimatorController.animationClips;
            for (int i = 0; i < clips.Length; i++)
            {
                bool isCorrectClip = string.Equals(clips[i].name, clipName);
                if (isCorrectClip)
                {
                    result = clips[i];
                    break;
                }
            }

            return result;
        }
    }
}