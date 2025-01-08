using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityTimer;

namespace GamePlay.Components
{
    public class Roll : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask = ~0;
        [SerializeField] private bool isPhysicsEnabled;
        [SerializeField] private float stepValue = 3.2f;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float cooldown = 1.3f;

        private Rigidbody _rigidbody;
        private Action _onComplete;
        private RaycastHit _hit;

        public bool IsExecuting { get; private set; }
        public bool IsAvailable { get; private set; }

        public void Initialize()
        {
            IsAvailable = true;
            if (isPhysicsEnabled)
                _rigidbody = this.GetComponent<Rigidbody>();
        }

        public void Initialize(float stepValue, float duration)
        {
            this.stepValue = stepValue;
            this.duration = duration;

            Initialize();
        }

        public void Execute
        (
            Vector3 direction
        )
        {
            IsExecuting = true;
            if (cooldown > 0)
            {
                IsAvailable = false;
                Timer.Register(cooldown, onComplete: () => IsAvailable = true);
            }

            direction *= stepValue;
            Vector3 startPosition = this.transform.position;
            Vector3 targetPosition = startPosition + direction;

            if (Physics.Raycast(startPosition, direction, out _hit, stepValue, layerMask))
            {
                targetPosition = _hit.point;
            }

            float time = CalculateTime(stepValue, duration, startPosition, targetPosition);
            if (isPhysicsEnabled)
            {
                _rigidbody.DOMove(targetPosition, duration).OnComplete(() =>
                {
                    IsExecuting = false;
                    _onComplete?.Invoke();
                }).SetEase(Ease.Linear);
            }
            else
            {
                this.transform.DOMove(targetPosition, duration).OnComplete(() =>
                {
                    IsExecuting = false;
                    _onComplete?.Invoke();
                }).SetEase(Ease.Linear);
            }
        }

        public void SetOnComplete(Action onComplete)
        {
            _onComplete = onComplete;
        }

        public void Cancel()
        {
            IsExecuting = false;
            IsAvailable = true;
            this.transform.DOKill();
            _rigidbody.DOKill();
        }

        private float CalculateTime
        (
            float moveUnit,
            float speed,
            Vector3 startPosition,
            Vector3 targetPosition
        )
        {
            float distance = Vector3.Distance(startPosition, targetPosition);
            float timeToReachTarget = distance / (moveUnit * speed);
            timeToReachTarget = Mathf.Clamp(timeToReachTarget, speed, timeToReachTarget);

            return timeToReachTarget;
        }
    }
}