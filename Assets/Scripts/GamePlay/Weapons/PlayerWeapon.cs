using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Characters;
using UnityEngine;

namespace GamePlay.Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject trailObject;
        [SerializeField] private Collider collider;
        [SerializeField] private float cooldown = .5f;
        [SerializeField] private float duration = .5f;
        [SerializeField] private float moveStepValue = 1f;

        private PlayerController _player;

        public bool IsExecuting { get; private set; }
        public bool IsAvailable { get; private set; }

        private const float START_DELAY = .2f;

        public void Initialize(PlayerController player)
        {
            IsAvailable = true;
            _player = player;
        }

        public async UniTask Attack()
        {
            IsExecuting = true;
            IsAvailable = false;
            _player.transform.DOMove(_player.View.transform.position + _player.View.transform.forward * moveStepValue, .2f);
            
            await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            trailObject.gameObject.SetActive(true);
            collider.enabled = true;
            
            await UniTask.Delay(TimeSpan.FromSeconds(duration - START_DELAY));
            trailObject.gameObject.SetActive(false);
            collider.enabled = false;
            IsExecuting = false;
            
            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            IsAvailable = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<IDamageable>();
            if (target == null)
                return;

            target.ApplyDamage(new DamageData(this.transform.parent.gameObject, 10));
        }
    }
}