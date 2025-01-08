namespace BaseY
{
    public interface IPoolable 
    {
        void Reinitialize();

        void OnSpawned();

        void OnDespawned();
    }
}