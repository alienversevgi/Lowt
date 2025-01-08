using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Components;
using GamePlay.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using Timer = UnityTimer.Timer;

namespace GamePlay.Characters
{
    public class PlayerController : Character, IDamageable
    {
        private readonly int KEY_ANIMATION_STATE = Animator.StringToHash("State");

        [SerializeField] private Animator animator;
        [SerializeField] private Transform view;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackMoveStepValue;
        [SerializeField] private float attackCooldown;
        [SerializeField] private GameObject trailObject;
        [SerializeField] private PlayerWeapon weapon;
        [SerializeField] private Roll roll;

        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private PlayerInputActions _playerInputActions;
        private Vector3 _smoothInputMovement;

        private bool _isMoving;
        private bool _isIdle;

        private bool _isMovingAvailable => !roll.IsExecuting && !weapon.IsExecuting;

        private bool _isRollingAvailable =>
            _isMoving && !weapon.IsExecuting && !roll.IsExecuting && roll.IsAvailable;

        private bool _isAttackAvailable => !roll.IsExecuting && weapon.IsAvailable;

        public Transform View => view;

        private void Awake()
        {
            _rigidbody = transform.GetComponent<Rigidbody>();
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Ingame.Attack01.performed += (param) => ExecuteAttack01(param).Forget();
            _playerInputActions.Ingame.Roll.performed += ExecuteRoll;
            roll.Initialize();
            roll.SetOnComplete(OnRollCompleted);
            weapon.Initialize(this);
        }

        private void ExecuteRoll(InputAction.CallbackContext obj)
        {
            if (!_isRollingAvailable || roll.IsExecuting)
                return;

            roll.Execute(view.forward);
            animator.SetInteger(KEY_ANIMATION_STATE, 2);
        }

        private async UniTask ExecuteAttack01(InputAction.CallbackContext obj)
        {
            if (!_isAttackAvailable)
                return;

            // SetIdleState();
            animator.speed = 1;
            animator.SetInteger(KEY_ANIMATION_STATE, 3);
            await weapon.Attack();
             SetIdleState();
        }

        private void Move()
        {
            if (!_isMovingAvailable)
                return;

            Vector2 input = _playerInputActions.Ingame.Movement.ReadValue<Vector2>();
            _direction = new Vector3(input.x, 0, input.y);

            bool hasValidDirection = _direction.magnitude > 0;
            _isMoving = hasValidDirection;

            if (!_isMoving)
            {
                SetIdleState();
                return;
            }

            view.transform.DOLookAt(transform.position + _direction, .3f, AxisConstraint.Y, Vector3.up);
            var x = _direction * moveSpeed * Time.deltaTime;
            _rigidbody.MovePosition(this.transform.position + x);
            animator.speed = 3 * _direction.magnitude;
            animator.SetInteger(KEY_ANIMATION_STATE, 1);
        }

        private void SetIdleState()
        {
            if (_isMoving)
                return;

            _isMoving = false;
            _isIdle = true;
            animator.speed = 1;
            animator.SetInteger(KEY_ANIMATION_STATE, 0);
        }

        private void FixedUpdate()
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

        private void OnRollCompleted()
        {
            SetIdleState();
        }

        public void ApplyDamage(DamageData data)
        {
            this.transform.DOPunchScale(Vector3.one * .5f, .2f, 5, 0);
        }
    }
}