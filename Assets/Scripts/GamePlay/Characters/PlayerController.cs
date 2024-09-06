using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Components;
using GamePlay.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityTimer;

namespace GamePlay.Characters
{
    public class PlayerController : Character, IDamageable
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
        [SerializeField] private float attackCooldown;
        [SerializeField] private float rollMoveDistanceMultiplier;
        [SerializeField] private float rollDuration;
        [SerializeField] private float rollCooldown;
        [SerializeField] private GameObject trailObject;
        [SerializeField] private PlayerWeapon weapon;
        [SerializeField] private Dash dash;

        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private PlayerInputActions _playerInputActions;
        private Vector3 _smoothInputMovement;

        private bool _isAttacking;
        private bool _hasAttack = true;
        private bool _isRolling;
        private bool _hasRoll = true;
        private bool _isMoving;
        private bool _isIdle;

        private bool _isMovingAvailable => !_isRolling && !_isAttacking;
        private bool _isRollingAvailable => _isMoving && !_isAttacking && !_isRolling && _hasRoll;
        private bool _isAttackAvailable => !_isRolling && !_isAttacking && _hasAttack;

        private void Awake()
        {
            _rigidbody = transform.GetComponent<Rigidbody>();
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Ingame.Attack01.performed += (param) => ExecuteAttack01(param).Forget();
            _playerInputActions.Ingame.Roll.performed += ExecuteRoll;
            dash.Initialize(() =>
                {
                    _isRolling = false;
                    animator.SetBool(KEY_ANIMATION_ROLL, false);
                }
            );
        }

        private void ExecuteRoll(InputAction.CallbackContext obj)
        {
            if (!_isRollingAvailable || _isRolling)
                return;

            _hasRoll = false;
            Timer.Register(rollCooldown, onComplete: () => _hasRoll = true);

            _isRolling = true;
            animator.SetBool(KEY_ANIMATION_ROLL, true);

            dash.Execute(view.forward, rollMoveDistanceMultiplier, rollDuration);
        }

        private async UniTask ExecuteAttack01(InputAction.CallbackContext obj)
        {
            if (!_isAttackAvailable)
                return;

            _hasAttack = false;
            Timer.Register(attackCooldown, () => _hasAttack = true);
            _isAttacking = true;
            animator.SetBool(KEY_ANIMATION_ATTACK, true);
            this.transform.DOMove(view.position + view.forward * attackMoveStepValue, .2f);
            await weapon.Attack();
            _isAttacking = false;
            animator.SetBool(KEY_ANIMATION_ATTACK, false);
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

        public void ApplyDamage(DamageData data)
        {
            this.transform.DOPunchScale(Vector3.one*.5f, .2f, 5, 0);
        }
    }
}