namespace GamePlay.Weapons
{
    public interface IWeapon<T> : IExecutableAction // TODO: Daha sonra bak 
    {
        void Initialize(T character);
    }
}