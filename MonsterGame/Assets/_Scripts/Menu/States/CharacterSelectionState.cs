using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Menu.States
{
    public class CharacterSelectionState : CustomState
    {
        [SerializeField] private bool _allowOnePlayer = false;
        
        
        [SerializeField] private TutorialMenuState _tutorialMenuState;
        [SerializeField] private GameObject characterSelection;
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private List<GameObject> _enableOnJoin;
        [SerializeField] private List<GameObject> _toEnable;
        [SerializeField] private List<RenderTexture> _textures;
        [Serialize] private List<Character> _characters;
        private int _ready;
        private bool _first;
        private int characterIndex;
        
        public override void StateReset()
        {
            
        }

        public override void StateStart()
        {
            characterSelection.SetActive(true);
            _characters = new List<Character>();
            EventBus<PlayerReadyUpEvent>.Subscribe(PlayersReady);
            EventBus<PlayerReadyDownEvent>.Subscribe(PlayerDown);
            
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
            InputSystem.onDeviceChange += OnDeviceChange;
            
            foreach (Gamepad gamepad in Gamepad.all)
            {
                characterIndex++;
                MenuStateManager.AddPlayer(_playerInputManager.JoinPlayer(characterIndex, -1, null, gamepad.device));
            }
        }

        public override void StateUpdate()
        {
            
        }

        public override void FixedStateUpdate()
        {
            
        }

        public override void Stop()
        {
            EventBus<PlayerReadyUpEvent>.UnSubscribe(PlayersReady);
            EventBus<PlayerReadyDownEvent>.UnSubscribe(PlayerDown);
            _playerInputManager.onPlayerJoined -= OnPlayerJoined;
            _playerInputManager.onPlayerLeft -= OnPlayerLeft;
            InputSystem.onDeviceChange -= OnDeviceChange;
        }
        
        private void PlayersReady(PlayerReadyUpEvent pEvent)
        {
            Debug.Log("player ready :)");
            _ready += 1;
            Character character = _characters.Find(x => x.ID.Equals(pEvent.ID));
            character.Display.SetActive(false);

            int maxReady = _allowOnePlayer ? 1 : 2;
            
            if (_playerInputManager.playerCount >= 2)
                maxReady = _playerInputManager.playerCount;
            if (_ready != maxReady)
                return;
            God.instance.ChosenCharacters.Clear();
            for (int i = 0; i < _characters.Count; i++)
            {
                God.instance.ChosenCharacters.Add(_characters[i].DeviceID, _characters[i].Scroller.CurrentCharacter);
            }
            MenuStateManager.SetState(typeof(TutorialMenuState));
        }
        
        private void PlayerDown(PlayerReadyDownEvent pEvent)
        {
            Character character = _characters.Find(x => x.ID.Equals(pEvent.ID));
            character.Display.SetActive(true);
            _ready -= 1;
        }
        
        private void OnPlayerJoined(PlayerInput pPlayerInput)
        {
            var transform1 = pPlayerInput.transform;
            SetupScroller(transform1);
            Scroller scroll = transform1.GetComponent<Scroller>();
            scroll.ID = Guid.NewGuid();
            Character character = new Character(scroll, _toEnable[_playerInputManager.playerCount - 1], scroll.ID);
            _enableOnJoin[_playerInputManager.playerCount - 1].SetActive(true);
            // Debug.Log(pPlayerInput.user.pairedDevices[0].name);
            character.DeviceID = pPlayerInput.user.pairedDevices[0].deviceId;
            _characters.Add(character);
            character.Display.SetActive(true);
            if (!_first)
            {
                scroll.First = true;
                _first = true;
            }
            characterIndex++;
        }
        
        private void OnPlayerLeft(PlayerInput pPlayerInput)
        {
            /*if(_toEnable[_playerInputManager.playerCount] != null) _toEnable[_playerInputManager.playerCount].SetActive(false);
            if(pPlayerInput.gameObject != null) Destroy(pPlayerInput.gameObject);*/
        }
        
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            Character character = _characters.Find(x =>
            {
                return x.DeviceID == device.deviceId;
            });
            switch (change)
            {
                case InputDeviceChange.Removed:
                    if (character == null)
                        return;
                    UpdateOrder(character);
                    break;
                case InputDeviceChange.Added:
                    if (character != null)
                        return;
                    characterIndex++;
                    MenuStateManager.AddPlayer(_playerInputManager.JoinPlayer(characterIndex, -1, null, device));
                    break;
            }
        }

        private void SetupScroller(Transform transform1)
        {
            transform1.parent = transform;
            transform1.position += new Vector3(0, 20 + 20 * characterIndex, 0);
            UnityEngine.Camera cam = transform1.GetComponentInChildren<UnityEngine.Camera>();
            cam.targetTexture = _textures[_playerInputManager.playerCount - 1];
        }

        private void UpdateOrder(Character character)
        {
            int index = 0;
            for (int i = 0; i < _characters.Count; i++)
            {
                Character ch = _characters[i];
                if (ch == character)
                {
                    index = i;
                }
            }
            if (index >= _characters.Count - 1)
            {
                character.Display.SetActive(false);
                Destroy(character.Scroller.gameObject);
                _enableOnJoin[index].SetActive(false);
                MenuStateManager.RemovePlayer(character.Scroller.GetComponent<PlayerInput>());
                _characters.Remove(character);
            }
            else
            {
                character.Display.SetActive(false);
                MenuStateManager.RemovePlayer(character.Scroller.GetComponent<PlayerInput>());
                Destroy(character.Scroller.gameObject);
                _characters.Remove(character);
                for (int i = index; i < 4; i++)
                {
                    _enableOnJoin[i].SetActive(false);
                    if (i < _characters.Count)
                    {
                        _enableOnJoin[i].SetActive(true);
                        Character tempChar = _characters[i];
                        if (i == 0)
                            tempChar.Scroller.First = true;
                        RenderTexture text = _textures[i];
                        GameObject arrows = _toEnable[i];
                        UnityEngine.Camera cam = tempChar.Scroller.GetComponentInChildren<UnityEngine.Camera>();
                        cam.targetTexture = text;
                        tempChar.Display.SetActive(false);
                        tempChar.Display = arrows;
                        if (!tempChar.Scroller.Ready)
                        {
                            tempChar.Display.SetActive(true);
                        }
                        else
                        {
                            tempChar.Display.SetActive(false);
                        }
                    }
                    else
                    {
                        _textures[i].Release();
                        _textures[i].Create();
                    }
                }
            }
        }
    }
}
