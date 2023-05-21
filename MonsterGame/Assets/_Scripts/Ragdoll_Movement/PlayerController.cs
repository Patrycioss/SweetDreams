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

		// private InputActionMap _playerActionMap;
		//
		// private InputAction _moveForward;
		// private InputAction _moveBackward;
		// private InputAction _moveLeft;
		// private InputAction _moveRight;
		// private InputAction _jump;
		// private InputAction _run;


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
			// _playerActionMap = _keyMap.FindActionMap("Player" + _playerNumber);
			//
			// if (_playerActionMap == null) Debug.LogError("Input map has no action map called 'Player'");
			// else
			// {
			// 	_playerActionMap.Enable();
			// 	
			// 	_moveForward = _playerActionMap.FindAction("move_forward");
			// 	_moveBackward = _playerActionMap.FindAction("move_backward");
			// 	_moveLeft = _playerActionMap.FindAction("move_left");
			// 	_moveRight = _playerActionMap.FindAction("move_right");
			// 	// _jump = _playerActionMap.FindAction("jump");
			// 	// _run = _playerActionMap.FindAction("run");
			// 	
			// }
		}

		public void Disable()
		{
			_hips.constraints = RigidbodyConstraints.None;
			_joint.targetRotation = Quaternion.Euler(90, _hips.rotation.eulerAngles.y, 0);

			DisableConstraints(transform);
			
			void DisableConstraints(Transform pTransform)
			{
				for (int i = 0; i < pTransform.childCount; i++)
				{
					Transform child = pTransform.GetChild(i);
					ConfigurableJoint joint = child.gameObject.GetComponent<ConfigurableJoint>();
					if (joint != null)
					{
						joint.angularXMotion = ConfigurableJointMotion.Free;
						joint.angularYMotion = ConfigurableJointMotion.Free;
						joint.angularZMotion = ConfigurableJointMotion.Free;
					}
					DisableConstraints(child);
				}
			}
			// transform.parent.rotation = Quaternion.Euler(0, transform.parent.rotation.eulerAngles.y, 0);
		}

		public void Move(Vector2 pDirection)
		{
			// Debug.Log($"Speed: {new Vector3(pDirection.x, 0, pDirection.y) * _speed}");
			float targetAngle = Mathf.Atan2(pDirection.y, pDirection.x) * Mathf.Rad2Deg;
				
			_joint.targetRotation = Quaternion.Euler(0, targetAngle - 90, 0);
			_hips.AddForce(new Vector3(pDirection.x, 0, pDirection.y) * _speed);
		}

		private void FixedUpdate()
		{
			return;
			// Vector3 direction = Vector3.zero;
			//
			// bool walking = false;
			//
			// if (_moveForward.IsPressed())
			// {
			// 	direction += Vector3.forward;
			// 	walking = true;
			// }
			//
			// if (_moveLeft.IsPressed())
			// {
			// 	direction += Vector3.left;
			// 	walking = true;
			// }
			//
			// if (_moveRight.IsPressed())
			// {
			// 	direction += Vector3.right;
			// 	walking = true;
			// }
			//
			// if (_moveBackward.IsPressed())
			// {
			// 	direction += Vector3.back;
			// 	walking = true;
			// }
			//
			// if (_playerNumber == 2)
			// {
			// 	float inputHor = Input.GetAxis("Horizontal");
			// 	float inputVer = Input.GetAxis("Vertical");
			// 	if (inputHor <= 0.01 && inputVer <= 0.01 && inputHor >= -0.01 && inputVer >= -0.01)
			// 		return;
			// 	direction = new Vector3(inputHor, 0, inputVer);
			// 	walking = true;
			// }
			//
			//
			// direction.Normalize();
			//
			// direction *= -1;
			//
			// if (walking)
			// {
			//
			// 	float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
			// 	
			// 	_joint.targetRotation = Quaternion.Euler(0, targetAngle - 270, 0);
			// 	_hips.AddForce(direction * _speed);
			// }
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
