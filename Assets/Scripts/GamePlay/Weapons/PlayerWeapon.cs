using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject trailObject;
        [SerializeField] private float attackDuration;
        [SerializeField] private float attackDelayDuration;
        [SerializeField] private Collider collider;
        
        public async UniTask Attack()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(attackDelayDuration));
            trailObject.gameObject.SetActive(true);
            collider.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(attackDuration - attackDelayDuration));
            trailObject.gameObject.SetActive(false);
            collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
             var target = other.GetComponent<IDamagable>();
             if (target == null)
                 return;
             
             target.ApplyDamage(10);
        }
    }
}