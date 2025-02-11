using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestBlood : MonoBehaviour, ICollectable
    {
        private bool _isGetted;
        private bool _isReady;
        private CurvedCollectableData _collectableData;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _collectableData = new CurvedCollectableData()
            {
                Type = CollectableType.Blood,
                TargetType = CollectableTargetType.Back,
                Owner = this.transform,
                StartPosition = this.transform.position,
                Speed = 3f,
                OnCompleted = Collect
            };
        }

        public void SetReady()
        {
            _isReady = true;
            _isGetted = false;
        }

        public void Collect()
        {
            Debug.Log("Collected");
            _isGetted = false;
        }

        public CollectableData GetCollectableData()
        {
            return _collectableData;
        }
    }
}