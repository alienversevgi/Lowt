using GamePlay.Characters.Enemys.RangedHuman;
using GamePlay.Weapons;
using Unity.Mathematics;
using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanView : MonoBehaviour
    {
        [SerializeField] private GameObject projectileObject;
        [SerializeField] private Transform aimPoint;
        
        public RangedHumanAnimationHandler AnimationHandler
        {
            get
            {
                if (_animationHandler == null)
                {
                    _animationHandler = this.GetComponent<RangedHumanAnimationHandler>();
                }

                return _animationHandler;
            }
        }

        private RangedHumanAnimationHandler _animationHandler;

        public RangedHumanProjectile GetProjectile()
        {
            var cloneObject = Instantiate(projectileObject, aimPoint.position, quaternion.identity);
            
            return cloneObject.GetComponent<RangedHumanProjectile>();
        }
    }
}