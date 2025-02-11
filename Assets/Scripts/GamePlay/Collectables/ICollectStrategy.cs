namespace DefaultNamespace
{
    public interface ICollectStrategy
    {
        void Initialize(CollectableData collectableData);
        void Execute(CollectableData collectableData, float deltaTime);
    }
}