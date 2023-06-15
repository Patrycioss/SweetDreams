using System;
using Unity.VisualScripting;
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
        [SerializeField] private Slider musicSlider, soundSlider;
        private Button _target;
        private PlayerInput _input;
        private InputActionMap _map;
        private InputAction _interact, _move;
        private long _cooldown;
        private bool _settingsMenuFocus;
        
        public override void StateReset()
        {
            menu.SetActive(true);
        }

        public override void StateStart()
        {
            _target = playButton;
            Image image = _target.GetComponent<Image>();
            image.sprite = playButton.spriteState.highlightedSprite;
            menu.SetActive(true);
            _input = playButton.GetComponent<PlayerInput>();
            _map = _input.actions.FindActionMap("Player");
            _interact = _map.FindAction("interaction");
            _move = _map.FindAction("move");
            _interact.started += SwitchButtonPressedActive;
            _interact.canceled += SwitchButtonPressedUnActive;
            _move.performed += Moving;
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
            _interact.started -= SwitchButtonPressedActive;
            _interact.canceled -= SwitchButtonPressedUnActive;
            _move.performed -= Moving;
            if(menu != null)
                menu.SetActive(false);
        }

        public void SetSettingFocus(bool settings)
        {
            _settingsMenuFocus = settings;
            //_target = musicSlider;
        }

        private void SwitchButtonPressedActive(InputAction.CallbackContext context)
        {
            Image targetImage = _target.GetComponent<Image>();
            targetImage.sprite = _target.spriteState.pressedSprite;
        }
        
        private void SwitchButtonPressedUnActive(InputAction.CallbackContext context)
        {
            Image targetImage = _target.GetComponent<Image>();
            targetImage.sprite = _target.spriteState.highlightedSprite;
            _target.onClick.Invoke();
        }

        private void Moving(InputAction.CallbackContext context)
        {
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() < _cooldown)
                return;
            Vector2 vector = _move.ReadValue<Vector2>();
            bool updated = false;
            Button oldTarget = _target;
            if (!_settingsMenuFocus)
            {
                if (_target.Equals(playButton))
                {
                    if (vector.x >= 0.1 && vector.y <= -0.5)
                    {
                        _target = quitButton;
                        updated = true;
                    }
                    else if (vector.x <= 0 && vector.y <= -0.5)
                    {
                        _target = settingsButton;
                        updated = true;
                
                    }
                }
                else if(_target.Equals(settingsButton))
                {
                    if (vector.x >= 0.5)
                    {
                        _target = quitButton;
                        updated = true;
                    }
                    else if (vector.y >= 0.5)
                    {
                        _target = playButton;
                        updated = true;
                
                    }
                }
                else if (_target.Equals(quitButton))
                {
                    if (vector.x <= -0.5)
                    {
                        _target = settingsButton;
                        updated = true;
                    }
                    else if (vector.y >= 0.5)
                    {
                        _target = playButton;
                        updated = true;
                
                    }
                }
            }
            else
            {
                
            }
            if (!updated)
                return;
            Image targetImage = oldTarget.GetComponent<Image>();
            targetImage.sprite = oldTarget.spriteState.disabledSprite;
            targetImage = _target.GetComponent<Image>();
            targetImage.sprite = _target.spriteState.highlightedSprite;
        }
    }
}
