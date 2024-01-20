using System;
using UnityEngine;

namespace GamePlay.Characters.AnimationBehaviours
{
    public class RangedHumanAttackAnimationBehaviour : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter
        (
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            animator.SetBool("IsAttackFrameReached", false);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate
        (
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            if (Math.Abs(stateInfo.normalizedTime - 0.5f) < .1f)
            {
                animator.SetBool("IsAttackFrameReached", true);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit
        (
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            animator.SetBool("IsAttackFrameReached", false);
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}