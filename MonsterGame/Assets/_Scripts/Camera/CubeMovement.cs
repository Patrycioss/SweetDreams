using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Camera
{
    public class CubeMovement : MonoBehaviour
    {
        private PlayerInput _player;
        private InputActionMap _inputActionMap;

        private InputAction _inputMove;

        private Rigidbody _rb;

        private void Awake()
        {
            _player = GetComponent<PlayerInput>();
            _rb = GetComponent<Rigidbody>();
            _inputActionMap = _player.actions.FindActionMap("Player");
            _inputActionMap.Enable();
            _inputMove = _inputActionMap.FindAction("move");
            _inputMove.performed += Move;
            _rb = GetComponent<Rigidbody>();
        }

        void Move(InputAction.CallbackContext cont)
        {
            Vector2 vector = _inputMove.ReadValue<Vector2>();
            Vector3 move = new Vector3(-vector.y, 0, vector.x);
            _rb.AddForce(move * 30);
        }
    }
}
