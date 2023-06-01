using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using _Scripts.Camera;
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
            IEnumerable<string> lines = File.ReadLines("Assets/Scenes/UserInterface/Backup.txt");
            foreach (string line in lines)
            {
                if (line.Length == 0)
                    return;
                string[] lineSplit = Regex.Split(line, ",");
                int id = Int32.Parse(lineSplit[0]);
                int character = Int32.Parse(lineSplit[1]);
                foreach (Gamepad gamepad in Gamepad.all)
                {
                    if (gamepad.deviceId == id)
                    {
                        _manager.playerPrefab = prefabs[character];
                        _manager.JoinPlayer(_characterIndex, -1, null, gamepad.device);
                        _characterIndex++;
                    }
                }
            }
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
