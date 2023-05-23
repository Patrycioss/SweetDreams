using UnityEngine;

namespace _Scripts.Car.Scripts
{
    public class CameraBhvr : MonoBehaviour
    {
        public Transform car;
        public float smoothing = .9f;

        void Start()
        {
        
        }

        void FixedUpdate()
        {
            transform.position = transform.position * (1f - smoothing) + car.position * smoothing;
            transform.rotation = car.rotation; 
        }
    }
}
