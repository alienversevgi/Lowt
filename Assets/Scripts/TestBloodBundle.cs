using System.Collections.Generic;
using _BaseY.Utils;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestBloodBundle : MonoBehaviour
    {
        public GameObject BloodPrefab;
        public float maxRadius;
        public float minRadius;
        public float jumpPower;
        public float duration;
        public int numJumps;
        private List<GameObject> Olds = new List<GameObject>();
        public int numbers;
        
        [Button]
        public void Multiple()
        {
            for (int i = 0; i < numbers; i++)
            {
                Push();
            }
        }
        
        [Button]
        public void Push()
        {
            var clone= Instantiate(BloodPrefab, this.transform.position, Quaternion.identity);
            clone.transform.localScale = Vector3.zero;
            clone.transform.DOScale(Vector3.one * .5f, duration *.6f).SetEase(Ease.OutBounce);
            clone.transform.DOJump(GetPoint(), jumpPower, numJumps, duration).SetEase(Ease.OutBack);
            
            Olds.Add(clone);
        }

        [Button]
        private Vector3 GetPoint()
        {
            float radius = Random.Range(minRadius, maxRadius);
            float angle = Random.Range(0, Mathf.PI * 2);
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            var result = this.transform.position + new Vector3(x, 0, y);
            
            return result.SetY(0);
        }

        [Button]
        public void RemoveOld()
        {
            for (int i = 0; i < Olds.Count; i++)
            {
                Destroy(Olds[i].gameObject);
            }
            
            Olds.Clear();
        }
        
    }
}