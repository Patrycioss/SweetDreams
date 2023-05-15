using System;
using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class CopyMotion : MonoBehaviour
    {
        [SerializeField] private Transform _followTarget;
        private ConfigurableJoint _joint;

        private void Start()
        {
            _joint = GetComponent<ConfigurableJoint>();
        }

        private void Update()
        {
            _joint.targetRotation = _followTarget.rotation;
        }
    }
}
