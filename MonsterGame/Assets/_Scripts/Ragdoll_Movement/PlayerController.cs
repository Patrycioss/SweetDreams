using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Ragdoll_Movement
{
	[RequireComponent(typeof(ConfigurableJoint))]
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private int _playerNumber = 1;
		[SerializeField] private Player _player;
		public Player player => _player;
		
		public int number => _playerNumber;
		
		[SerializeField] private InputActionAsset _keyMap;
		[SerializeField] private float _speed = 150;

		private Rigidbody _hips;
		private ConfigurableJoint _joint;
	
		private void OnValidate()
		{
			if (_keyMap == null) 
				Debug.LogError("Player Controls not assigned!");
		}

		private void Awake()
		{
			_hips = GetComponent<Rigidbody>();
			_joint = GetComponent<ConfigurableJoint>();
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
		}

		public void Move(Vector2 pDirection)
		{
			// Debug.Log($"Speed: {new Vector3(pDirection.x, 0, pDirection.y) * _speed}");
			float targetAngle = Mathf.Atan2(pDirection.y, pDirection.x) * Mathf.Rad2Deg;
				
			_joint.targetRotation = Quaternion.Euler(0, targetAngle - 90, 0);
			_hips.AddForce(new Vector3(pDirection.x, 0, pDirection.y) * _speed);
		}
		
		public float Speed
		{
			get => _speed;
			set => _speed = value;
		}
	}
}
