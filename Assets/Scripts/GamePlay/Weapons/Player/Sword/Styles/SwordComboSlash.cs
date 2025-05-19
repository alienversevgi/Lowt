using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Characters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace GamePlay.Weapons.Player.Styles
{
    public class SwordComboSlash : MonoBehaviour, IWeaponStyle<PlayerController>
    {
        [SerializeField] private List<VisualEffect> slashEffects;
        [SerializeField] private Collider collider;
        [SerializeField] private float cooldown = .5f;
        [SerializeField] private float duration = .5f;
        [SerializeField] private float moveStepValue = 1f;
        [SerializeField] private float startDelay = .2f;
        [SerializeField] private float nextInterruptDuration = .25f;
        [SerializeField] private float slashEndDuration;
        [SerializeField] private float comboResetDuration = .1f;

        private PlayerController _player;
        private int _currentCombo;
        private int _requestedCombo;
        private bool _hasRequestedCombo;
        private bool _cooldownCompleted;

        private delegate UniTask Slash();

        private Slash _first;
        private Slash _second;


        public void Initialize(PlayerController player)
        {
            _player = player;
            _currentCombo = 0;
            _hasRequestedCombo = false;
            _cooldownCompleted = true;
            slashEndDuration = duration - startDelay;
            _first = () => ExecuteSlash(PlayerAnimationState.SwordSlash01, 0);
            _second = () => ExecuteSlash(PlayerAnimationState.SwordSlash02, 1);
        }

        public async UniTask Execute()
        {
            if (!_cooldownCompleted)
            {
                return;
            }

            if (_currentCombo > 0 && !_hasRequestedCombo)
            {
                _hasRequestedCombo = true;
                await UniTask.WaitWhile(() => _currentCombo > 0);
                return;
            }

            if (_currentCombo > 0)
            {
                await UniTask.WaitWhile(() => _currentCombo > 0);
                return;
            }

            await _first.Invoke();
            if (!await CheckRequestedCombo())
            {
                Reset();
                return;
            }

            await _second.Invoke();
            if (!await CheckRequestedCombo())
            {
                Reset();
                return;
            }

            await _first.Invoke();
            Reset();
        }

        private async UniTask<bool> CheckRequestedCombo()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(comboResetDuration));
            return _hasRequestedCombo;
        }

        private async UniTask StartCooldown()
        {
            _cooldownCompleted = false;
            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            _cooldownCompleted = true;
        }

        public bool IsExecuting()
        {
            return _currentCombo > 0;
        }

        public bool IsAvailable()
        {
            return _currentCombo < 3;
        }

        private async UniTask ExecuteSlash(PlayerAnimationState animationState, int slashIndex)
        {
            _hasRequestedCombo = false;
            _currentCombo++;
            _player.transform.DOMove(_player.View.transform.position + _player.View.transform.forward * moveStepValue,
                .2f);
            _player.View.AnimationHandler.Play(animationState);
            await UniTask.Delay(TimeSpan.FromSeconds(startDelay));
            slashEffects[slashIndex].enabled = true;
            slashEffects[slashIndex].Play();
            collider.enabled = true;
            float waitDuration = _hasRequestedCombo ? slashEndDuration - nextInterruptDuration : slashEndDuration;
            await UniTask.Delay(TimeSpan.FromSeconds(waitDuration));
            collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<IDamageable>();
            if (target == null)
                return;

            target.ApplyDamage(new DamageData(this.transform.parent.gameObject, 10));
        }

        private void Reset()
        {
            _hasRequestedCombo = false;
            StartCooldown().Forget();
            _currentCombo = 0;
        }

        private void SwapSlashes()
        {
            (_first, _second) = (_second, _first);
        }

#if UNITY_EDITOR

        [Button]
        private void SetSlashEffectDuration(float effectDuration)
        {
            var slashes = this.GetComponentsInChildren<VisualEffect>(true);
            for (int i = 0; i < slashes.Length; i++)
            {
                slashes[i].SetFloat("slash life time", effectDuration);
            }
        }
#endif
    }
}