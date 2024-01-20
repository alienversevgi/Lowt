using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamePlay.Characters
{
    public class PlayerController : Character
    {
        private readonly int KEY_ANIMATION_MOVEMENT = Animator.StringToHash("IsMoving");
        private readonly int KEY_ANIMATION_ATTACK = Animator.StringToHash("IsAttacking");
        private readonly int KEY_ANIMATION_ROLL = Animator.StringToHash("IsRolling");

        [SerializeField] private Animator animator;
        [SerializeField] private Transform view;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackDuration;
        [SerializeField] private float attackDelayDuration;
        [SerializeField] private float attackMoveStepValue;
        [SerializeField] private float rollMoveDistanceMultiplier;
        [SerializeField] private float rollDuration;
        [SerializeField] private GameObject trailObject;

        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private PlayerInputActions _playerInputActions;
        private Vector3 _smoothInputMovement;

        private bool _isAttacking;
        private bool _isRolling;
        private bool _isMoving;
        private bool _isIdle;

        private bool _isMovingAvailable => !_isRolling && !_isAttacking;
        private bool _isRollingAvailable => !_isAttacking;
        private bool _isAttackAvailable => !_isRolling;

        private void Awake()
        {
            _rigidbody = transform.GetComponent<Rigidbody>();
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Ingame.Attack01.performed += (param) => ExecuteAttack01(param).Forget();
            _playerInputActions.Ingame.Roll.performed += ExecuteRoll;
        }

        private void ExecuteRoll(InputAction.CallbackContext obj)
        {
            if (_isRollingAvailable && !_isRolling)
            {
                _isRolling = true;
                animator.SetBool(KEY_ANIMATION_ROLL, true);

                this.transform.DOMove(view.position + view.forward * rollMoveDistanceMultiplier, rollDuration)
                    .OnComplete(() =>
                        {
                            _isRolling = false;
                            animator.SetBool(KEY_ANIMATION_ROLL, false);
                        }
                    );
            }
        }

        private async UniTask ExecuteAttack01(InputAction.CallbackContext obj)
        {
            if (_isAttackAvailable && !_isAttacking)
            {
                _isAttacking = true;
                animator.SetBool(KEY_ANIMATION_ATTACK, true);
                this.transform.DOMove(view.position + view.forward * attackMoveStepValue, .2f);
                await UniTask.Delay(TimeSpan.FromSeconds(attackDelayDuration));
                trailObject.gameObject.SetActive(true);
                await UniTask.Delay(TimeSpan.FromSeconds(attackDuration - attackDelayDuration));
                _isAttacking = false;
                trailObject.gameObject.SetActive(false);
                animator.SetBool(KEY_ANIMATION_ATTACK, false);
            }
        }

        private void Move()
        {
            Vector2 input = _playerInputActions.Ingame.Movement.ReadValue<Vector2>();
            _direction = new Vector3(input.x, 0, input.y);

            if (_isMovingAvailable)
            {
                bool hasValidDirection = _direction.magnitude > 0;
                _isMoving = hasValidDirection;

                if (hasValidDirection)
                {
                    view.transform.DOLookAt(transform.position + _direction, .3f, AxisConstraint.Y, Vector3.up);
                    var x = _direction * moveSpeed * Time.deltaTime;
                    _rigidbody.MovePosition(this.transform.position + x);
                    animator.speed = 3 * _direction.magnitude;
                }

                animator.SetBool(KEY_ANIMATION_MOVEMENT, hasValidDirection);
            }
            else
            {
                _isMoving = false;
                _isIdle = true;
                animator.speed = 1;
                animator.SetBool(KEY_ANIMATION_MOVEMENT, false);
            }
        }

        private void Update()
        {
            Move();
        }

        private void OnEnable()
        {
            _playerInputActions.Ingame.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Ingame.Disable();
        }
    }
}