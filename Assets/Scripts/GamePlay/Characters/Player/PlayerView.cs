using UnityEngine;

namespace GamePlay.Characters
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerAnimationHandler AnimationHandler
        {
            get
            {
                if (_animationHandler == null)
                {
                    _animationHandler = this.GetComponent<PlayerAnimationHandler>();
                }

                return _animationHandler;
            }
        }

        private PlayerAnimationHandler _animationHandler;
    }
}