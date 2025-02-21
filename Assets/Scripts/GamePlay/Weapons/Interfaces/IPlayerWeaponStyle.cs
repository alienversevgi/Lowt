using Cysharp.Threading.Tasks;
using GamePlay.Characters;

namespace GamePlay.Weapons
{
    public interface IPlayerWeaponStyle // TODO: Daha sonra bak 
    {
        void Initialize(PlayerController player);
        UniTask PerformAttack();
        
        bool IsExecuting();
        bool IsAvailable();
    }
}