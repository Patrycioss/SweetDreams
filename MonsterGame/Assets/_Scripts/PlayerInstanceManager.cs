using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using _Scripts.Camera;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class PlayerInstanceManager : MonoBehaviour
    {
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
                int id = Int32.Parse(line);
                foreach (Gamepad gamepad in Gamepad.all)
                {
                    if (gamepad.deviceId == id)
                    {
                        PlayerInput input = _manager.JoinPlayer(_characterIndex, -1, null, gamepad.device);
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
