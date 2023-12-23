using UnityEngine;

namespace GamePlay.Character.Enemy
{
    public class RangedHumanAnimationHandler : MonoBehaviour
    {
        public readonly int Idle = Animator.StringToHash("IsIdle");
        public readonly int Prepare = Animator.StringToHash("IsPrepare");
        public readonly int Walk = Animator.StringToHash("IsWalk");
        public readonly int Run = Animator.StringToHash("IsRun");
        public readonly int Throw = Animator.StringToHash("IsThrow");

        [SerializeField] private Animator animator;

        public void PlayTrigger(int id)
        {
            animator.SetTrigger(id);
        }

        public void PlayBool(int id, bool isOn)
        {
            animator.SetBool(id, isOn);
        }
    }
}