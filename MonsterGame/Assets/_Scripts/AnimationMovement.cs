using System;
using _Scripts.Ragdoll_Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
	[RequireComponent(typeof(Animator))]
	public class AnimationMovement : MonoBehaviour
	{
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private InputActionAsset _inputActionAsset;
		private InputActionMap _inputActionMap;

		private Animator _animator;
		private static readonly int _Walking = Animator.StringToHash("Walking");

		private InputAction _moveForward;
		private InputAction _moveBackward;
		private InputAction _moveLeft;
		private InputAction _moveRight;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			
		}

		private void Start()
		{
			if (_inputActionAsset != null)
			{
				_inputActionMap = _inputActionAsset.FindActionMap("Player" + _playerController.number);
				if (_inputActionMap != null)
				{
					_inputActionMap.Enable();
					
					_moveForward = _inputActionMap.FindAction("move_forward");
					_moveBackward = _inputActionMap.FindAction("move_backward");
					_moveLeft = _inputActionMap.FindAction("move_left");
					_moveRight = _inputActionMap.FindAction("move_right");
				}
				else Debug.LogError("Player Action Map not found!");
			}
		}

		private void FixedUpdate()
		{
			if (_moveForward.IsPressed() || _moveBackward.IsPressed() || _moveLeft.IsPressed() || _moveRight.IsPressed())
			{
				_animator.SetBool(_Walking, true);
			}
			else _animator.SetBool(_Walking, false);
		}
	}
}