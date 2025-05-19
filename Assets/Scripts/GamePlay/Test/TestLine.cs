using UnityEngine;
using UnityEngine.InputSystem;

namespace GamePlay
{
    public class TestLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        [SerializeField] private LineRenderer line2;
        [SerializeField] private Camera camera;

        private PlayerInputActions _playerInputActions;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Ingame.AimWithMouse.performed += AimWithMouse;
            _playerInputActions.Ingame.AimWithGamepad.performed += AimWithGamepad;
            
        }

        private void AimWithMouse(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            value = ScreenToAimDirection(value);
            Debug.Log(value);
            Aim(value);
        }
        
        private void AimWithGamepad(InputAction.CallbackContext context)
        {
            Aim(context.ReadValue<Vector2>());
        }
        
        private Vector2 ScreenToAimDirection(Vector2 screenPosition)
        {
            Vector2 screenCenter = new Vector2(Screen.width * .5f, Screen.height * .5f);
            Vector2 direction = (screenPosition - screenCenter) / screenCenter;
            
            return direction; 
        }
        
        private void Aim(Vector2 value)
        {
            var point2 = new Vector3(value.x, 0, value.y);
            // point2 += this.transform.position;

            line2.SetPosition(1, point2);
        }
        
        
        // private void Aim(Vector2 value)
        // {
        //     var screen = camera.ScreenToWorldPoint(new Vector3(value.x, value.y, 13.5f));
        //     var viewport = camera.ViewportToWorldPoint(new Vector3(value.normalized.x, value.normalized.y, 0));
        //     //var test =  camera.ViewportToWorldPoint(new Vector3(value.x, value.y, 0)); 
        //     var direction = value.normalized;
        //     screen = screen.normalized;
        //     Debug.Log($"AimRay {value} direction {direction}");
        //     var point1 = new Vector3(screen.x, 0, screen.z);
        //     var point2 = new Vector3(direction.x, 0, direction.y);
        //     // point2 += this.transform.position;
        //
        //     line.SetPosition(1, point1);
        //     line2.SetPosition(1, point2);
        // }
        
        private void OnEnable()
        {
            _playerInputActions.Ingame.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Ingame.Disable();
        }
    }
}