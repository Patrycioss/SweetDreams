using System;
using UnityEngine;

namespace _Scripts.Menu
{
    public class Character
    {
        private int _deviceId;
        private Guid _guid;
        private Scroller _scroller;
        private GameObject _display;

        public Character(Scroller scroller, GameObject display, int deviceId)
        {
            _scroller = scroller;
            _display = display;
            _guid = _scroller.ID;
            _deviceId = deviceId;
        }

        public Guid ID
        {
            set => _guid = value;
            get => _guid;
        }

        public int DeviceID
        {
            set => _deviceId = value;
            get => _deviceId;
        }

        public Scroller Scroller
        {
            set => _scroller = value;
            get => _scroller;
        }

        public GameObject Display
        {
            set => _display = value;
            get => _display;
        }
    }
}
