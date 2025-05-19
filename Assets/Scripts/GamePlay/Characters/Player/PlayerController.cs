using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Components;
using GamePlay.Weapons;
using GamePlay.Weapons.Player;
using GamePlay.Weapons.Player.Test;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamePlay.Characters
{
    public class PlayerController : Character, IDamageable
    {
        [SerializeField] private PlayerView view;
        [SerializeField] private float moveSpeed;
        [SerializeField] private PlayerHitReactionHandler hitReaction;
        [SerializeField] private PlayerSwordWeapon sword;
        [SerializeField] private PlayerWeaponTest testWeapon;
        [SerializeField] private Roll roll;

        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private PlayerInputActions _playerInputActions;
        private Vector3 _smoothInputMovement;

        private bool _isMoving;
        private bool _isIdle;

        private bool _isMovingAvailable => !_isHitReactionExecuting && !roll.IsExecuting && !sword.IsExecuting();

        private bool _isRollingAvailable =>
            !_isHitReactionExecuting && !sword.IsExecuting() && !roll.IsExecuting && roll.IsAvailable;

        private bool _isAttackAvailable => !_isHitReactionExecuting && !roll.IsExecuting;

        private bool _isHitReactionExecuting => hitReaction.IsExecuting;

        public PlayerView View => view;
        public PlayerInputActions Input => _playerInputActions;

        private void Awake()
        {
            _rigidbody = transform.GetComponent<Rigidbody>();
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Ingame.Attack01.performed += (param) => ExecuteAttack01(param).Forget();
            _playerInputActions.Ingame.Attack02Focus.started += Attack02Started;
            _playerInputActions.Ingame.Attack02Focus.performed += (param) => ExecuteAttack02(param).Forget();
            _playerInputActions.Ingame.Attack02Focus.canceled += Attack02Canceled;
            _playerInputActions.Ingame.Roll.performed += ExecuteRoll;
            hitReaction.Initialize(this);
            roll.Initialize();
            roll.SetOnComplete(OnRollCompleted);
            testWeapon.Initialize(this);
            sword.Initialize(this);
            sword.SetStyle(PlayerSwordStyle.Combo);
        }

        private void Attack02Started(InputAction.CallbackContext obj)
        {
            testWeapon.Started();
        }

        private void Attack02Canceled(InputAction.CallbackContext obj)
        {
            testWeapon.Cancel();
        }

        private void ExecuteRoll(InputAction.CallbackContext obj)
        {
            if (!_isRollingAvailable || roll.IsExecuting)
                return;

            roll.Execute(view.transform.forward);
            view.AnimationHandler.Play(PlayerAnimationState.Rolling);
        }

        private async UniTask ExecuteAttack01(InputAction.CallbackContext obj)
        {
            if (!_isAttackAvailable && !sword.IsAvailable())
                return;

            await sword.Execute();
        }

        private async UniTask ExecuteAttack02(InputAction.CallbackContext obj)
        {
            if (!_isAttackAvailable && !testWeapon.IsAvailable())
                return;

            await testWeapon.Execute();
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

            this.transform.DOLookAt(transform.position + _direction, .2f, AxisConstraint.Y, Vector3.up);
            var newPosition = this.transform.position + (_direction * moveSpeed * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
            view.AnimationHandler.Play(PlayerAnimationState.Moving, 3 * _direction.magnitude);
        }

        private void SetIdleState()
        {
            if (_isMoving || _isHitReactionExecuting)
                return;

            _isMoving = false;
            _isIdle = true;
            view.AnimationHandler.Play(PlayerAnimationState.Idle);
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
            if (_isHitReactionExecuting)
                return;

            hitReaction.Execute(data).Forget();
        }
    }
}