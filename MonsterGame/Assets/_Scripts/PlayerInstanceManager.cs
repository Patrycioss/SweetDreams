using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerInstanceManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabs;
        private PlayerInputManager _manager;

        private int _characterIndex;
        // Start is called before the first frame update
        void Start()
        {
            _manager = GetComponent<PlayerInputManager>();
            foreach (KeyValuePair<int, int> pair in God.instance.ChosenCharacters)
            {
                foreach (Gamepad gamepad in Gamepad.all)
                {
                    if (gamepad.deviceId != pair.Key)
                        continue;
                    _manager.playerPrefab = prefabs[pair.Value];
                    PlayerInput playerInput = _manager.JoinPlayer(_characterIndex, -1, null, gamepad.device);
                    TrackablePlayerCircle circle = playerInput.GetComponentInChildren<TrackablePlayerCircle>();
                    circle.SetActive(_characterIndex);
                    _characterIndex++;
                }
            }
        }
    }
}
