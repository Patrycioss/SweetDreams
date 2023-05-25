using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Menu
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class UIPlayerManager : MonoBehaviour
    {
        /*[SerializeField] private List<GameObject> _toEnable;
        [SerializeField] private List<RenderTexture> _textures;
        [Serialize] private List<Character> _characters;
        private PlayerInputManager _playerInputManager;
        private int _ready;
        private bool _first;

        private void Awake()
        {
            _characters = new List<Character>();
            EventBus<PlayerReadyUpEvent>.Subscribe(PlayersReady);
            EventBus<PlayerReadyDownEvent>.Subscribe(PlayerDown);
            _playerInputManager = GetComponent<PlayerInputManager>();
            
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
            InputSystem.onDeviceChange += OnDeviceChange;
            foreach (Gamepad gamepad in Gamepad.all)
            {
                _playerInputManager.JoinPlayer();
            }
        }
        
        private void OnPlayerJoined(PlayerInput pPlayerInput)
        {
            Debug.Log("Booger");
            var transform1 = pPlayerInput.transform;
            SetupScroller(transform1);
            Scroller scroll = transform1.GetComponent<Scroller>();
            scroll.ID = Guid.NewGuid();
            Character character = new Character(pPlayerInput.gameObject, _toEnable[_playerInputManager.playerCount - 1], scroll.ID);    
            _characters.Add(character);
            character.Display.SetActive(true);
            if (!_first)
            {
                scroll.First = true;
                _first = true;
            }
        }
        
        private void OnPlayerLeft(PlayerInput pPlayerInput)
        {
            if(_toEnable[_playerInputManager.playerCount] != null) _toEnable[_playerInputManager.playerCount].SetActive(false);
            if(pPlayerInput.gameObject != null) Destroy(pPlayerInput.gameObject);
        }
        
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            switch (change)
            {
                case InputDeviceChange.Removed:
                    Character character = _characters.Find(x =>
                    {
                        return x.DeviceID == device.deviceId;
                    });
                    character.Display.SetActive(false);
                    break;
                case InputDeviceChange.Added:
                    Debug.Log(_characters.Count);
                    _characters[_characters.Count - 1].DeviceID = device.deviceId;
                    break;
                case InputDeviceChange.Reconnected:
                    Debug.Log(_characters.Count);
                    _characters[_characters.Count - 1].DeviceID = device.deviceId;
                    break;
            }
        }
        
        private void OnDisable()
        {
            _playerInputManager.onPlayerJoined -= OnPlayerJoined;
            _playerInputManager.onPlayerLeft -= OnPlayerLeft;
            EventBus<PlayerReadyUpEvent>.UnSubscribe(PlayersReady);
            EventBus<PlayerReadyDownEvent>.UnSubscribe(PlayerDown);
        }

        private void PlayersReady(PlayerReadyUpEvent pEvent)
        {
            _ready += 1;
            Character character = _characters.Find(x => x.ID.Equals(pEvent.ID));
            character.Display.SetActive(false);
            if (_ready != _playerInputManager.playerCount)
                return;
            SceneManager.LoadScene("Prototype");
        }
        
        private void PlayerDown(PlayerReadyDownEvent pEvent)
        {
            Character character = _characters.Find(x => x.ID.Equals(pEvent.ID));
            character.Display.SetActive(true);
            _ready -= 1;
        }

        private void SetupScroller(Transform transform1)
        {
            transform1.parent = transform;
            transform1.position += new Vector3(0, 10 * _playerInputManager.playerCount);
            UnityEngine.Camera cam = transform1.GetComponentInChildren<UnityEngine.Camera>();
            cam.targetTexture = _textures[_playerInputManager.playerCount - 1];
        }*/
    }
}