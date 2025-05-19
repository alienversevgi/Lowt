using System;
using Cysharp.Threading.Tasks;
using GamePlay.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay
{
    public class TestDamageDealer : MonoBehaviour
    {
        [SerializeField] private float waitStart = .2f;
        [SerializeField] private float waitEnd = .2f;
        [SerializeField] private bool isSelected;
        
        private IDamageable _target;
        private Action _resetPositon; 

        public void Initialize(IDamageable damageable, Action resetPosition)
        {
            _target = damageable;
            _resetPositon = resetPosition;
        }

        public void Update()
        {
            if (!isSelected)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                DealDamage();
            }
        }


        [Button]
        public void DealDamage()
        {
            Run().Forget();
        }

        private async UniTask Run()
        {
            _resetPositon.Invoke();
            await UniTask.Delay(TimeSpan.FromSeconds(waitStart));
            _target.ApplyDamage(new DamageData(this.gameObject, 10));
        }
    }
}