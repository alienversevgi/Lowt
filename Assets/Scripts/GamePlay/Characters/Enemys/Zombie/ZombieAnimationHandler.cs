namespace GamePlay.Characters.Enemys
{
    public class ZombieAnimationHandler : AnimationHandler
    {
        public void Play(ZombieStateType stateType)
        {
            Play((int)stateType);
        }
    }
}