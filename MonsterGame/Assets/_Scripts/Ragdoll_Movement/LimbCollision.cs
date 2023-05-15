using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
    public class LimbCollision : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private void OnValidate()
        {
            if (_playerController == null) 
                Debug.LogError("Player Controller not assigned!");
        }

        private void OnCollisionEnter(Collision pCollision)
        {
            _playerController.RegisterCollision(name);
        }
    }
}
