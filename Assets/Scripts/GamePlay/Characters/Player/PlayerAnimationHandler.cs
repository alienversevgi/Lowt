namespace GamePlay.Characters
{
    public class PlayerAnimationHandler : AnimationHandler
    {
        public void Play(PlayerAnimationState state, float speed = 1)
        {
            Play((int)state, speed);
        }
    }
}