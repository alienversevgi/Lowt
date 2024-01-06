using UnityEngine;

namespace GamePlay.Characters.Enemys
{
    public class RangedHumanView : MonoBehaviour
    {
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
    }
}