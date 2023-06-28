using System;
using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        
        public Transform target
        {
            get => _target;
            set => _target = value;
        }
        
        private float _distance;


        private void Start()
        {
            _distance = Vector3.Distance(transform.position, _target.position);
            
        }

        private void Update()
        {
            transform.position = _target.position - Vector3.forward * _distance;
        }
    }
}
