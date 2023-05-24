using System;
using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Ragdoll_Movement
{
    public class Limb : MonoBehaviour
    {
        public Transform followTarget;
        public Player player;

        private ConfigurableJoint _joint;
        private Quaternion _targetInitialRotation;
        private void Awake()
        {
            _joint = GetComponent<ConfigurableJoint>();
        }

        private void Start()
        {
            _targetInitialRotation = followTarget.localRotation;
        }

        private void FixedUpdate()
        {
            _joint.targetRotation = CopyRotation();
        }

        private Quaternion CopyRotation()
        {
            return Quaternion.Inverse(followTarget.localRotation) * _targetInitialRotation;
        }
    }
} 
