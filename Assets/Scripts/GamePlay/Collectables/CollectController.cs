using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public class CollectController : MonoBehaviour
    {
        [SerializeField] private List<Transform> targets;

        private List<CollectableData> _collectables;
        private Dictionary<CollectableType, ICollectStrategy> _strategies;
        private ICollectStrategy _strategy;

        private void Awake()
        {
            _collectables = new List<CollectableData>();

            _strategies = new Dictionary<CollectableType, ICollectStrategy>()
            {
                { CollectableType.Blood, new CurvedCollectStrategy() }
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                Subscribe(collectable);
            }
        }

        private void Update()
        {
            for (int i = 0; i < _collectables.Count; i++)
            {
                if (_strategies.TryGetValue(_collectables[i].Type, out _strategy))
                    _strategy.Execute(_collectables[i], Time.deltaTime);

                if (_collectables[i].IsFinished)
                {
                    _collectables[i].OnCompleted?.Invoke();
                }
            }
        }

        private void LateUpdate()
        {
            RemoveUnnecessaryData();
        }

        [Button]
        public void Subscribe(ICollectable collectable)
        {
            var collectData = collectable.GetCollectableData();
            if (_collectables.Contains(collectData))
                return;

            collectData.Type = collectData.Type;

            if (collectData.TargetType != CollectableTargetType.Custom)
                collectData.Target = targets[(int)collectData.TargetType];
            
            if (_strategies.TryGetValue(collectData.Type, out _strategy))
                _strategy.Initialize(collectData);

            _collectables.Add(collectData);
        }
        
        private void RemoveUnnecessaryData()
        {
            var tempList = new List<CollectableData>(_collectables);
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].IsFinished)
                {
                    _collectables.Remove(tempList[i]);
                }
            }
        }
    }
}