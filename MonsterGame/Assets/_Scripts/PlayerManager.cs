using System.Collections.Generic;
using _Scripts.Camera;
using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private CameraMovement _cameraMovement;
        private PlayerInputManager _playerInputManager;
        
        private void Awake()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();
            
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
        }
        
        private void OnPlayerJoined(PlayerInput pPlayerInput)
        {
            Player player = pPlayerInput.GetComponent<Player>();
            if (player == null) Debug.LogError("Player script not found on player prefab in OnPlayerJoined in PlayerManager.");
            else
            {
                _cameraMovement.AddPlayer(player.controller.gameObject);
                player.transform.SetParent(transform);
            }
        }
        
        private void OnPlayerLeft(PlayerInput pPlayerInput)
        {
            _cameraMovement.RemovePlayer(pPlayerInput.gameObject);
            Destroy(pPlayerInput.gameObject);
        }
        
        private void OnDisable()
        {
            _playerInputManager.onPlayerJoined -= OnPlayerJoined;
            _playerInputManager.onPlayerLeft -= OnPlayerLeft;
        }
    }
}
