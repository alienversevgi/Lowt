using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamePlay.Weapons.Player.Test
{
    public class PlayerWeaponTest : MonoBehaviour, IWeapon<PlayerController>
    {
        [SerializeField] private GameObject rayIndicator;
        [SerializeField] private LineRenderer line;
        [SerializeField] private Camera camera;
        [SerializeField] private float lineLength;
        
        private PlayerController _player;

        private bool _isAiming;

        public void Initialize(PlayerController character)
        {
            _player = character;
        }

        private void AimWithMouse(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            value = ScreenToAimDirection(value);
            AimRay(value);
        }
        
        private void AimWithGamepad(InputAction.CallbackContext context)
        {
            AimRay(context.ReadValue<Vector2>());
        }
        
        private Vector2 ScreenToAimDirection(Vector2 screenPosition)
        {
            Vector2 screenCenter = new Vector2(Screen.width * .5f, Screen.height * .5f);
            Vector2 direction = (screenPosition - screenCenter) / screenCenter;
            
            return direction; 
        }

        public void Started()
        {
            Debug.Log("TestWeapon Started");
            _player.Input.Ingame.AimWithMouse.performed += AimWithMouse;
            _player.Input.Ingame.AimWithGamepad.performed += AimWithGamepad;
            _player.Input.Ingame.Attack02Execute.performed += AttackPerformed;
        }

        private void AttackPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log("Attack Performed");
            CancelAim();
        }

        public async UniTask Execute()
        {
            Debug.Log("TestWeapon Executing");
            _isAiming = true;
            await UniTask.CompletedTask;
        }
        
        public void Cancel()
        {
            Debug.Log("TestWeapon Cancel");
            CancelAim();
        }

        public bool IsExecuting()
        {
            return _isAiming;
        }

        public bool IsAvailable()
        {
            return !_isAiming;
        }
        
        private void AimRay(Vector2 value)
        {
            var direction = new Vector3(value.x, 0, value.y);
            Vector3 point = this.transform.position + direction * lineLength;
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, point);

            rayIndicator.transform.position = direction;
        }
        
        private void CancelAim()
        {
            _player.Input.Ingame.AimWithMouse.performed -= AimWithMouse;
            _player.Input.Ingame.AimWithGamepad.performed -= AimWithGamepad;
            _player.Input.Ingame.Attack02Execute.performed -= AttackPerformed;
        }
    }
}