using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        public float speed = 600.0f;
        public float turnSpeed = 400.0f;
        private Vector3 moveDirection = Vector3.zero;
        public float gravity = 20.0f;

        // void Start()
        // {
        //     controller = GetComponent<CharacterController>();
        // }
        //
        // void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         anim.SetTrigger("Attack");
        //         AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //         float animLength = stateInfo.length;
        //     }
        //
        //     
        //     
        //     if (Input.GetKey("w"))
        //     {
        //         anim.SetInteger("AnimationPar", 1);
        //     }
        //     else
        //     {
        //         anim.SetInteger("AnimationPar", 0);
        //     }
        //
        //     if (controller.isGrounded)
        //     {
        //         moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        //     }
        //
        //     float turn = Input.GetAxis("Horizontal");
        //     transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        //     controller.Move(moveDirection * Time.deltaTime);
        //     moveDirection.y -= gravity * Time.deltaTime;
        // }
    }
}