using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanAnimationHandler : MonoBehaviour
    {
        private readonly int _state = Animator.StringToHash("State");
        
        [SerializeField] private Animator animator;
        
        public void PlayTrigger(int id)
        {
            animator.SetTrigger(id);
        }

        public void Play(RangedHumanStateType stateType)
        {
            animator.SetInteger(_state, (int) stateType);
        }

        public void PlayBool(int id, bool isOn)
        {
            animator.SetBool(id, isOn);
        }

        public bool GetBool(string key)
        {
           return animator.GetBool(key);
        }
    }
}