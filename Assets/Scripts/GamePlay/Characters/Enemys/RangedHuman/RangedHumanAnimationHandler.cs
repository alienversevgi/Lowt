namespace GamePlay.Characters.Enemys
{
    public class RangedHumanAnimationHandler : AnimationHandler
    {
        public void Play(RangedHumanStateType stateType)
        {
            Play((int)stateType);
        }
    }
}