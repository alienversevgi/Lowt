using Cysharp.Threading.Tasks;

namespace GamePlay.Weapons
{
    public interface IExecutableAction
    {
        UniTask Execute();
        bool IsExecuting();
        bool IsAvailable();
    }
}