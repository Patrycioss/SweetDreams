using System;
using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private float _strafeSpeed;
		[SerializeField] private float _jumpForce;

		[SerializeField] private bool _isGrounded;

		private Rigidbody _hips;

		private void Start()
		{
			_hips = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			
		}
	}
}
