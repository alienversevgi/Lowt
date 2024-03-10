using Pathfinding;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanFlee : RangedHumanState
    {
        [SerializeField] private int magnitude;
        [SerializeField] private int offset;
        [SerializeField] private int tryCount;
        [SerializeField] private LayerMask layerMask;
        
        public override void Enter()
        {
            var fleePoint = GetFleePoint();
            _controller.Move(fleePoint, OnReachedToFleePoint);
        }

        private Vector3 GetFleePoint()
        {
            var randomPointOnMyBack = TryGetRandomPointOnSide(true, tryCount);
            if (randomPointOnMyBack != Vector3.zero)
            {
                return randomPointOnMyBack;
            }

            var randomPointOnMyFront = TryGetRandomPointOnSide(false, tryCount);
            if (randomPointOnMyFront != Vector3.zero)
            {
                return randomPointOnMyFront;
            }

            var fleePoint = GetRandomPointCircle(tryCount);
            
            return fleePoint;
        }

        private Vector3 TryGetRandomPointOnSide(bool isBack, int tryCount)
        {
            Vector3 result = Vector3.zero;
            for (int i = 0; i < tryCount; i++)
            {
                var randomPointOnMySide = GetRandomPointOnSide(isBack);
                var isPointReachable = IsPointReachable(randomPointOnMySide);
                if (isPointReachable)
                {
                    result = randomPointOnMySide;
                    break;
                }
            }

            return result;
        }

        private bool IsPointReachable(Vector3 point)
        {
            var heading = point -this.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance; 
            
            Ray ray = new Ray(this.transform.position, direction);

            bool noCastingObject = !Physics.SphereCast(ray, 1f,distance,layerMask);
            if (noCastingObject)
            {
                var isPointReachable = IsPathPossible(point);
                return isPointReachable;
            }

            return false;
        }

        private bool IsPathPossible(Vector3 point)
        {
            var startNode = AstarPath.active.GetNearest(this.transform.position).node;
            var finishNode = AstarPath.active.GetNearest(point).node;
            bool isPointReachable = PathUtilities.IsPathPossible(startNode, finishNode);
            return isPointReachable;
        }

        private Vector3 GetRandomPointOnSide(bool isBack)
        {
            int directionMultiplier = isBack ? -1 : 1;
            var foundedRandomPoint = Random.insideUnitSphere * magnitude;

            foundedRandomPoint.y = 0;
            foundedRandomPoint.z = (Mathf.Abs(foundedRandomPoint.z) + offset) * directionMultiplier;
            foundedRandomPoint = this.transform.TransformPoint((Vector3.forward * directionMultiplier) + foundedRandomPoint);

            return foundedRandomPoint;
        }

        private Vector3 GetRandomPointCircle(int tryCount)
        {
            var foundedRandomPoint = Vector3.zero;
            for (int i = 0; i < tryCount; i++)
            {
                foundedRandomPoint = Random.insideUnitSphere * magnitude * offset;
                foundedRandomPoint.y = 0;
                if (IsPathPossible(foundedRandomPoint))
                {
                    break;
                }
            }

            return foundedRandomPoint;
        }

        private void OnReachedToFleePoint()
        {
            _stateController.ChangeState(nameof(RangedHumanPrepare));
        }
    }
}