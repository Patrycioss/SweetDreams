using System;
using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
    public class CopyMotion : MonoBehaviour
    {
        public Transform _followTarget;

        private ConfigurableJoint _joint;

        private Quaternion _targetInitialRotation;
        
        private void Awake()
        {
            
            
            _joint = GetComponent<ConfigurableJoint>();
            // Debug.Log($"Joint: {_joint}");
            //
            // Debug.Log($"Target: {_followTarget}");

            
      
            

        }

        private void Start()
        {
            _targetInitialRotation = _followTarget.localRotation;

        }

        private void FixedUpdate()
        {
            _joint.targetRotation = CopyRotation();
        }

        private Quaternion CopyRotation()
        {
            return Quaternion.Inverse(_followTarget.localRotation) * _targetInitialRotation;
        }
    }
} 
