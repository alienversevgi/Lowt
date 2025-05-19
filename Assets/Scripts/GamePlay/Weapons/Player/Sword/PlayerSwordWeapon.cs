using Cysharp.Threading.Tasks;
using GamePlay.Characters;
using UnityEngine;

namespace GamePlay.Weapons.Player
{
    public enum PlayerSwordStyle
    {
        Combo
    }

    public class PlayerSwordWeapon : MonoBehaviour, IWeapon<PlayerController>
    {
        private IWeaponStyle<PlayerController>[] _styles;
        private PlayerController _player;
        private IWeaponStyle<PlayerController> _current;
        private PlayerSwordStyle _currentSwordStyle;

        public bool IsExecuting() => _current.IsExecuting();

        public bool IsAvailable() => _current.IsAvailable();

        public void Initialize(PlayerController player)
        {
            _player = player;
            _styles = this.GetComponentsInChildren<IWeaponStyle<PlayerController>>();
            for (int i = 0; i < _styles.Length; i++)
            {
                _styles[i].Initialize(_player);
            }
        }

        public void SetStyle(PlayerSwordStyle style)
        {
            _currentSwordStyle = style;
            _current = _styles[(int)style];
        }

        public async UniTask Execute()
        {
            await _current.Execute();
        }
    }
}