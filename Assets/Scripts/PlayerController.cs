using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private readonly int KEY_ANIMATION_MOVEMENT = Animator.StringToHash("IsMoving");
    private readonly int KEY_ANIMATION_ATTACK = Animator.StringToHash("IsAttacking");
    private readonly int KEY_ANIMATION_ROLL = Animator.StringToHash("IsRolling");

    [SerializeField] private Animator animator;
    [SerializeField] private Transform view;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float ATTACK_DURATION = 1.0f;
    [SerializeField] private float attackMoveStepValue;
    [SerializeField] private float rollMoveDistanceMultiplier;
    [SerializeField] private float rollAfterAnimatorSpeedChangeDuration;
    [SerializeField] private float ROLL_DURATION = 1.0f;

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
        _playerInputActions.Ingame.Attack01.performed += ExecuteAttack01;
        _playerInputActions.Ingame.Roll.performed += ExecuteRoll;
    }

    private void ExecuteRoll(InputAction.CallbackContext obj)
    {
        if (_isRollingAvailable && !_isRolling)
        {
            _isRolling = true;
            animator.SetBool(KEY_ANIMATION_ROLL, true);
            // animator.SetBool(KEY_ANIMATION_MOVEMENT, false);
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animLength = stateInfo.length;
            Debug.Log("Roll " + animLength);
            //
            // this.transform.DOMove(view.position + view.forward * rollMoveDistanceMultiplier, ROLL_DURATION * .5f)
            //     .OnComplete(() => _isRolling = false);

            this.transform.DOMove(view.position + view.forward * rollMoveDistanceMultiplier, ROLL_DURATION).OnComplete(() =>
            {
                _isRolling = false;
                animator.SetBool(KEY_ANIMATION_ROLL, false);
            });
        }
    }

    private void ExecuteAttack01(InputAction.CallbackContext obj)
    {
        if (_isAttackAvailable && !_isAttacking)
        {
            Debug.Log("Attack");
            _isAttacking = true;
            animator.SetBool(KEY_ANIMATION_ATTACK, true);

            this.transform.DOMove(view.position + view.forward * attackMoveStepValue, .2f);
            WaitAndExecute(ATTACK_DURATION,
                           () =>
                           {
                               _isAttacking = false;
                               animator.SetBool(KEY_ANIMATION_ATTACK, false);
                           }
                )
                .Forget();
        }
    }

    private void OnEnable()
    {
        _playerInputActions.Ingame.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Ingame.Disable();
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
                this.transform.Translate(_direction * moveSpeed * Time.deltaTime);
                animator.speed = 3 * _direction.magnitude;
            }
   
            animator.SetBool(KEY_ANIMATION_MOVEMENT, hasValidDirection);
        }
        else
        {
            _isMoving = false;
            _isIdle = true;
            // animator.speed = 1;
            animator.SetBool(KEY_ANIMATION_MOVEMENT, false);
        }
    }

    private void Update()
    {
        Move();
    }

    private async UniTask WaitAndExecute(float waitDuration, System.Action action)
    {
        await UniTask.Delay((int) (waitDuration * 1000));
        action?.Invoke();
    }
}