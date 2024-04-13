using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Components
{
    [RequireComponent(typeof(Collider))]
    public class Range : MonoBehaviour
    {
        public List<GameObject> Objects { get; private set; }

        private bool _isEnable;
        private Collider _collider;

        private void Awake()
        {
            Objects = new List<GameObject>();
            _collider = this.GetComponent<Collider>();
        }

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            _collider.enabled = isEnable;
            Objects.Clear();
        }

        private void OnTriggerStay(Collider other)
        {
            Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Remove(other);
        }

        private void Add(Collider other)
        {
            if (Objects.Contains(other.gameObject))
                return;
            
            Objects.Add(other.gameObject);
        }

        private void Remove(Collider other)
        {
            if (!Objects.Contains(other.gameObject))
                return;

            Objects.Remove(other.gameObject);
        }

        public async UniTask<bool> IsOnRange(GameObject target)
        {
            SetEnable(true);
            await UniTask.DelayFrame(3);
            bool result = Objects.Contains(target);
            SetEnable(false);
            return result;
        }
    }
}