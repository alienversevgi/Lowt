using GamePlay.Character.Enemy;
using UnityEngine;

namespace GamePlay.Character
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