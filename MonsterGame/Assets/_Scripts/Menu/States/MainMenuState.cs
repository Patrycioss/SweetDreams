using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace _Scripts.Menu.States
{
    public class MainMenuState : CustomState
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private Button playButton, settingsButton, quitButton;
        private Button _target;
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
            _input = playButton.GetComponent<PlayerInput>();
            _map = _input.actions.FindActionMap("Player");
            _interact = _map.FindAction("interaction");
            _interact.performed += Invoking;
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
            _interact.performed -= Invoking;
            if(menu != null)
                menu.SetActive(false);
        }

        private void Invoking(InputAction.CallbackContext context)
        {
            playButton.onClick.Invoke();
        }
    }
}
