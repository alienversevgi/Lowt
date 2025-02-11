using UnityEngine;

namespace DefaultNamespace
{
    public class CurvedCollectStrategy : ICollectStrategy
    {
        private const float CURVE_RIGHT_VALUE = 5.0f;
        private const float CURVE_UP_VALUE = 2.5f;

        public void Initialize(CollectableData collectableData)
        {
            if (collectableData is not CurvedCollectableData data)
                throw new System.InvalidCastException("Invalid data type for CurvedMovementStrategy in Initialize.");

            data.IsRight = data.Owner.position.x - data.Target.position.x > 0;
        }

        public void Execute(CollectableData collectableData, float deltaTime)
        {
            if (collectableData is not CurvedCollectableData data)
                throw new System.InvalidCastException("Invalid data type for CurvedMovementStrategy in Execute.");

            data.Progress += data.Speed * deltaTime;
            data.Progress = Mathf.Clamp01(data.Progress);

            var rightCurve = data.Target.right * (CURVE_RIGHT_VALUE * (data.IsRight ? 1 : -1));
            var upCurve = Vector3.up * CURVE_UP_VALUE;

            Vector3 midPoint = data.Target.position + rightCurve + upCurve;
            Vector3 pos1 = Vector3.Lerp(data.StartPosition, midPoint, data.Progress);
            Vector3 pos2 = Vector3.Lerp(midPoint, data.Target.position, data.Progress);

            data.Owner.position = Vector3.Lerp(pos1, pos2, data.Progress);

            if (data.Progress >= 1f)
            {
                data.IsFinished = true;
                data.Owner.gameObject.SetActive(false);
            }
        }
    }
}