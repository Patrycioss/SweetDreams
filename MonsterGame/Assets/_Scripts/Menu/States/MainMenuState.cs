using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace _Scripts.Menu.States
{
    public class MainMenuState : CustomState
    {
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private GameObject menu;
        [SerializeField] private Button button;
        private PlayerInput _input;
        private InputActionMap _map;
        private InputAction _interact;
        public override void StateReset()
        {
            menu.SetActive(true);
        }

        public override void StateStart()
        {
            menu.SetActive(true);
            _input = button.GetComponent<PlayerInput>();
            _map = _input.actions.FindActionMap("Player");
            _interact = _map.FindAction("interaction");
            _interact.performed += (InputAction.CallbackContext context) =>
            {
                button.onClick.Invoke();
            };
        }

        public override void StateUpdate()
        {
            //Nothin
        }

        public override void FixedStateUpdate()
        {
            //Nothin
        }

        public override void Stop()
        {
            menu.SetActive(false);
        }
    }
}
