using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Ragdoll_Movement
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private InputActionAsset _keyMap;
		
		[SerializeField] private float _speed = 150;
		[SerializeField] private float _strafeSpeed = 100;
		[SerializeField] private float _jumpForce = 0;

		[SerializeField] private bool _isGrounded;

		private Rigidbody _hips;

		private InputActionMap _playerActionMap;

		private InputAction _moveForward;
		private InputAction _moveBackward;
		private InputAction _moveLeft;
		private InputAction _moveRight;
		private InputAction _jump;
		private InputAction _run;

		private void OnValidate()
		{
			if (_keyMap == null) Debug.LogError("Player Controls not assigned!");
		}

		private void Awake()
		{
			_hips = GetComponent<Rigidbody>();
			_playerActionMap = _keyMap.FindActionMap("Player");
			
			if (_playerActionMap == null) Debug.LogError("Input map has no action map called 'Player'");
			else
			{
				_playerActionMap.Enable();
				
				_moveForward = _playerActionMap.FindAction("move_forward");
				_moveBackward = _playerActionMap.FindAction("move_backward");
				_moveLeft = _playerActionMap.FindAction("move_left");
				_moveRight = _playerActionMap.FindAction("move_right");
				_jump = _playerActionMap.FindAction("jump");
				_run = _playerActionMap.FindAction("run");
			}
		}

		private void FixedUpdate()
		{
			if (_moveForward.IsPressed())
			{
				Debug.Log( "works");
				_hips.AddForce(_hips.transform.forward * _speed);
			}
		}
	}
}
