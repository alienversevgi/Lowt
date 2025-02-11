using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public abstract class CollectableData
    {
        public CollectableType Type;
        public CollectableTargetType TargetType;
        public Transform Owner;
        public Transform Target;
        public Vector3 StartPosition;
        public float Progress;
        public float Speed;
        public bool IsFinished;
        public Action OnCompleted;
    }

    [Serializable]
    public class CurvedCollectableData : CollectableData
    {
        public bool IsRight;
    }
}