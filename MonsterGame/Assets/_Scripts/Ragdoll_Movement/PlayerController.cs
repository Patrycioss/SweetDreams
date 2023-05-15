using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Ragdoll_Movement
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private InputActionAsset _keyMap;

		[SerializeField] private Animator _animator;
		
		[SerializeField] private float _speed = 150;
		[SerializeField] private float _strafeSpeed = 100;
		[SerializeField] private float _jumpForce = 0;
		[SerializeField] private float _runSpeedModifier = 1.5f;

		private bool _isGrounded = true;

		private Rigidbody _hips;

		private InputActionMap _playerActionMap;

		private InputAction _moveForward;
		private InputAction _moveBackward;
		private InputAction _moveLeft;
		private InputAction _moveRight;
		private InputAction _jump;
		private InputAction _run;

		private Action _do;
		private Action<float, string, float> _doFloat;
		
		private static readonly int _Walking = Animator.StringToHash("Walking");

		public void RegisterCollision(string pLimbName)
		{
			if (pLimbName == "LeftFoot" || pLimbName == "RightFoot")
			{
				_isGrounded = true;
			}
		}
		
		private void OnValidate()
		{
			if (_keyMap == null) Debug.LogError("Player Controls not assigned!");
			if (_animator == null) Debug.LogError("Animator not assigned!");
		}

		private void Awake()
		{
			_do += () =>
			{
				Debug.Log("hoi");
			};
			
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
				// _animator?.SetBool(_Walking, true);
				
				if (_run.IsPressed()) {
					_hips.AddForce(_hips.transform.forward * (_speed * _runSpeedModifier));
				}
				else _hips.AddForce(_hips.transform.forward * _speed);
			}
			// else _animator.SetBool(_Walking,false);

			if (_moveLeft.IsPressed())
			{
				_hips.AddForce(_hips.transform.right * -_strafeSpeed);
			}
			
			if (_moveRight.IsPressed())
			{
				_hips.AddForce(_hips.transform.right * _strafeSpeed);
			}
			
			if (_moveBackward.IsPressed())
			{
				_hips.AddForce(_hips.transform.forward * -_speed);
			}

			if (_jump.IsPressed())
			{
				if (_isGrounded)
				{
					_hips.AddForce(_hips.transform.up * _jumpForce);
					_isGrounded = false;
				}
				 
			}
		}
	}
}
