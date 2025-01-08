using UnityEngine;

namespace GamePlay.Characters
{
    public class AnimationHandler : MonoBehaviour
    {
        private readonly int _state = Animator.StringToHash("State");

        [SerializeField] protected Animator animator;

        public void PlayTrigger(int id)
        {
            animator.SetTrigger(id);
        }

        public void Play(int state)
        {
            animator.SetInteger(_state, state);
        }

        public void Play(int id, bool isOn)
        {
            animator.SetBool(id, isOn);
        }

        public bool GetBool(string key)
        {
            return animator.GetBool(key);
        }

        public float GetCurrentAnimationLength()
        {
            var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            return clipInfo[0].clip.length;
        }

        public AnimationClip GetAnimationClip(string clipName)
        {
            AnimationClip result = null;
            var clips = animator.runtimeAnimatorController.animationClips;
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