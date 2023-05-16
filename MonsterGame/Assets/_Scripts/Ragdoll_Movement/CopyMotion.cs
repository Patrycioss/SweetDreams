using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class CopyMotion : MonoBehaviour
    {
        public Transform _followTarget;

        private void Update()
        {
            transform.localPosition = _followTarget.localPosition;
            transform.localRotation = _followTarget.localRotation;
        }
    }
}
