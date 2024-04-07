using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class ZombieView : MonoBehaviour
    {
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
    }
}