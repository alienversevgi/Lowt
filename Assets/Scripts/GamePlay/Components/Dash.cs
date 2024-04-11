using System;
using DG.Tweening;
using UnityEngine;

namespace GamePlay.Components
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask = ~0;
        [SerializeField] private bool isPhysicsEnabled;
        
        private Rigidbody _rigidbody;
        private Action _onComplete;

        public void Initialize(Action onComplete = null)
        {
            if (isPhysicsEnabled)
                _rigidbody = this.GetComponent<Rigidbody>();
            
            _onComplete = onComplete;
        }

        public void Execute
        (
            Vector3 direction,
            float moveUnit,
            float speed
        )
        {
            direction *= moveUnit;
            Vector3 startPosition = this.transform.position;
            Vector3 targetPosition = startPosition + direction;

            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction, out hit, moveUnit, layerMask))
            {
                targetPosition = hit.point;
            }

            float time = CalculateTime(moveUnit, speed, startPosition, targetPosition);
            if (isPhysicsEnabled)
            {
                _rigidbody.DOMove(targetPosition, time).OnComplete(() => { _onComplete?.Invoke(); });
            }
            else
            {
                this.transform.DOMove(targetPosition, time).OnComplete(() => { _onComplete?.Invoke(); });
            }
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
            float timeToReachTarget = distance / (moveUnit / speed);
            timeToReachTarget = Mathf.Clamp(timeToReachTarget, speed, timeToReachTarget);
            
            return timeToReachTarget;
        }
    }
}