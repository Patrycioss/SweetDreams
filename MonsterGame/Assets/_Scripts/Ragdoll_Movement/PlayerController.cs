using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Ragdoll_Movement
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private int _playerNumber = 1;
		public int number => _playerNumber;
		
		[SerializeField] private InputActionAsset _keyMap;

		[SerializeField] private Animator _animator;
		
		[SerializeField] private float _speed = 150;
		[SerializeField] private float _strafeSpeed = 100;
		[SerializeField] private float _jumpForce = 0;
		[SerializeField] private float _runSpeedModifier = 1.5f;

		[SerializeField] private float _smoothingFactor = .1f;
		
		
		
		private bool _isGrounded = true;

		private Rigidbody _hips;

		private InputActionMap _playerActionMap;

		private InputAction _moveForward;
		private InputAction _moveBackward;
		private InputAction _moveLeft;
		private InputAction _moveRight;
		private InputAction _jump;
		private InputAction _run;


		private Transform _leftShin;
		private Transform _rightShin;

		private ConfigurableJoint _joint;
		

		
		
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
			// if (_animator == null) Debug.LogError("Animator not assigned!");
		}

		private void Awake()
		{
			_hips = GetComponent<Rigidbody>();
			_joint = GetComponent<ConfigurableJoint>();
			_playerActionMap = _keyMap.FindActionMap("Player" + _playerNumber);
			
			if (_playerActionMap == null) Debug.LogError("Input map has no action map called 'Player'");
			else
			{
				_playerActionMap.Enable();
				
				_moveForward = _playerActionMap.FindAction("move_forward");
				_moveBackward = _playerActionMap.FindAction("move_backward");
				_moveLeft = _playerActionMap.FindAction("move_left");
				_moveRight = _playerActionMap.FindAction("move_right");
				// _jump = _playerActionMap.FindAction("jump");
				// _run = _playerActionMap.FindAction("run");
				
			}
		}

		private void FixedUpdate()
		{
			Vector3 direction = Vector3.zero;
			
			bool walking = false;
			

			if (_moveForward.IsPressed())
			{
				direction += Vector3.forward;
				walking = true;
			}
			
			if (_moveLeft.IsPressed())
			{
				direction += Vector3.left;
				walking = true;
			}
			
			if (_moveRight.IsPressed())
			{
				direction += Vector3.right;
				walking = true;
			}
			
			if (_moveBackward.IsPressed())
			{
				direction += Vector3.back;
				walking = true;
			}

			direction.Normalize();
			
			if (walking)
			{

				float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
				
				_joint.targetRotation = Quaternion.Euler(0, targetAngle - 90, 0);
				_hips.AddForce(direction * _speed);
			}
			//
			// if (_jump.IsPressed())
			// {
			// 	if (_isGrounded)
			// 	{
			// 		_hips.AddForce(_hips.transform.up * _jumpForce);
			// 		_isGrounded = false;
			// 	}
			// 	 
			// }
		}
	}
}
