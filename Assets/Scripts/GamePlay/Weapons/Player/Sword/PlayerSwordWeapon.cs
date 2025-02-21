using Cysharp.Threading.Tasks;
using GamePlay.Characters;
using UnityEngine;

namespace GamePlay.Weapons.Player
{
    public enum PlayerSwordStyle
    {
        Combo
    }

    public class PlayerSwordWeapon : MonoBehaviour
    {
        private IPlayerWeaponStyle[] _styles;
        private PlayerController _player;
        private IPlayerWeaponStyle _currentStyle;
        private PlayerSwordStyle _currentSwordStyle;
        public bool IsExecuting => _currentStyle.IsExecuting();
        public bool IsAvailable => _currentStyle.IsAvailable();

        public void Initialize(PlayerController player)
        {
            _player = player;
            _styles = this.GetComponentsInChildren<IPlayerWeaponStyle>();
            for (int i = 0; i < _styles.Length; i++)
            {
                _styles[i].Initialize(_player);
            }
        }

        public void SetStyle(PlayerSwordStyle style)
        {
            _currentSwordStyle = style;
            _currentStyle = _styles[(int)style];
        }

        public async UniTask Attack()
        {
            await _currentStyle.PerformAttack();
        }
    }
}