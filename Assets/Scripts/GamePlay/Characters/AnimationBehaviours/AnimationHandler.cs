using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class AnimationHandler : MonoBehaviour
    {
        private readonly int _state = Animator.StringToHash("State");
        
        [SerializeField] private Animator animator;
        
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
    }
}