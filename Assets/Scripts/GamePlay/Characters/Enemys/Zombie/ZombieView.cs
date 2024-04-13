using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class ZombieView : MonoBehaviour
    {
        [SerializeField] private List<Material> materials;
        [SerializeField] private Renderer renderer;
        [SerializeField] private RagdollEnabler ragdoll;
        
        private MaterialPropertyBlock _materialPropertyBlock;

        public ZombieAnimationHandler AnimationHandler
        {
            get
            {
                if (_animationHandler == null)
                {
                    _animationHandler = this.GetComponent<ZombieAnimationHandler>();
                }

                return _animationHandler;
            }
        }

        private ZombieAnimationHandler _animationHandler;
        
        private void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            SetHitMaterialEffect(0);
        }

        private void SetHitMaterialEffect(float value)
        {
            _materialPropertyBlock.SetFloat("_Value", value);
            for (int i = 0; i < materials.Count; i++)
            {
                renderer.SetPropertyBlock(_materialPropertyBlock, i);
            }
        }

        public async UniTask PlayHitReaction()
        {
            var current = _materialPropertyBlock.GetFloat("_Value");
            var target = current + .5f;
            SetHitMaterialEffect(target);
            await UniTask.Delay(TimeSpan.FromSeconds(AnimationHandler.GetCurrentAnimationLength()));
        }

        public void RunDead()
        {
            // AnimationHandler.Play(ZombieStateType.Dead);
            ragdoll.Enable();
        }
    }
}