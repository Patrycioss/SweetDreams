using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Scripts.Menu.States
{
    public class MainMenuState : CustomState
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private Selectable playButton, settingsButton, quitButton, musicSlider, soundSlider, _back;
        private Selectable _target;
        private PlayerInput _input;
        private InputActionMap _map;
        private InputAction _interact, _move, _backAction;
        private long _cooldown;
        private bool _settingsMenuFocus;
        
        public override void StateReset()
        {
            menu.SetActive(true);
        }

        public override void StateStart()
        {
            if (PlayerPrefs.HasKey("sound"))
                soundSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sound");
            if (PlayerPrefs.HasKey("music"))
                musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("music");
            _target = playButton;
            Image image = _target.GetComponent<Image>();
            image.sprite = playButton.spriteState.highlightedSprite;
            menu.SetActive(true);
            _input = playButton.GetComponent<PlayerInput>();
            _map = _input.actions.FindActionMap("Player");
            _interact = _map.FindAction("interaction");
            _move = _map.FindAction("move");
            _backAction = _map.FindAction("b");
            _interact.started += SwitchButtonPressedActive;
            _interact.canceled += SwitchButtonPressedUnActive;
        }

        public override void StateUpdate()
        {
            Moving();
        }

        public override void FixedStateUpdate()
        {
            //Nothin
        }

        public override void Stop()
        {
            _interact.started -= SwitchButtonPressedActive;
            _interact.canceled -= SwitchButtonPressedUnActive;
            if(menu != null)
                menu.SetActive(false);
        }

        public void SetSettingFocus(bool settings)
        {
            _settingsMenuFocus = settings;
            _target = musicSlider;
        }

        public void SaveSettings()
        {
            Slider musicSlider = this.musicSlider.GetComponentInChildren<Slider>();
            Slider soundSlider = this.soundSlider.GetComponentInChildren<Slider>();
            PlayerPrefs.SetFloat("music", musicSlider.value);
            PlayerPrefs.SetFloat("sound", soundSlider.value);
            PlayerPrefs.Save();
        }

        private void SwitchButtonPressedActive(InputAction.CallbackContext context)
        {
            Image targetImage = _target.GetComponent<Image>();
            if (targetImage != null)
                targetImage.sprite = _target.spriteState.pressedSprite;
        }
        
        private void SwitchButtonPressedUnActive(InputAction.CallbackContext context)
        {
            Image targetImage = _target.GetComponent<Image>();
            if (targetImage != null)
            {
                targetImage.sprite = _target.spriteState.highlightedSprite;
                if (_target.Equals(playButton) || _target.Equals(settingsButton) || _target.Equals(quitButton))
                {
                    Button button = _target.GetComponentInChildren<Button>();
                    button.onClick.Invoke();
                }
            }
        }

        private struct MenuNav
        {
            private bool[] _conditions;
            private Selectable[] _targets;
            public MenuNav(params Selectable[] targets)
            {
                _targets = targets;
                _conditions = Array.Empty<bool>();
            }

            public MenuNav SetConditions(params bool[] conditions)
            {
                _conditions = conditions;
                return this;
            }

            public bool[] Conditions => _conditions;
            public Selectable[] Selectables => _targets;
        }

        private bool SetNewTarget(MenuNav nav)
        {
            bool[] conditions = nav.Conditions;
            Selectable[] selectables = nav.Selectables;
            for (int i = 0; i < conditions.Length; i++)
            {
                if (conditions[i])
                {
                    _target = selectables[i];
                    _cooldown = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 400;
                    return true;
                }
            }
            return false;
        }

        private void Moving()
        {
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() < _cooldown)
                return;
            Selectable oldTarget = _target.GetComponentInChildren<Button>();
            if(oldTarget == null)
                oldTarget = _target.GetComponentInChildren<Slider>();
            if(oldTarget == null)
                oldTarget = _target.GetComponentInChildren<Toggle>();
            Vector2 vector = _move.ReadValue<Vector2>();
            bool updated = false;
            if (!_settingsMenuFocus)
            {
                if (_target.Equals(playButton))
                    updated = SetNewTarget(new MenuNav(quitButton, settingsButton).SetConditions(
                        (vector.x >= 0.1 && vector.y <= -0.5), (vector.x <= 0 && vector.y <= -0.5)));
                else if(_target.Equals(settingsButton))
                    updated = SetNewTarget(new MenuNav(quitButton, playButton).SetConditions(
                        vector.x >= 0.5f, vector.y >= 0.5f));
                else if (_target.Equals(quitButton))
                    updated = SetNewTarget(new MenuNav(settingsButton, playButton).SetConditions(
                        vector.x <= -0.5f, vector.y >= 0.5f));
            }
            else
            {
                if (_backAction.IsPressed())
                {
                    _back.GetComponentInChildren<Button>().onClick.Invoke();
                    _target = settingsButton;
                    _settingsMenuFocus = false;
                }
                if (_target.Equals(musicSlider) || _target.Equals(soundSlider))
                {
                    //DDD
                    float addition = 0.6f * vector.x * Time.deltaTime;
                    Slider slider = _target.GetComponentInChildren<Slider>();
                    if(vector.x >= 0.1f || vector.x <= -0.1f)
                        slider.value += addition;
                }
                if (_target.Equals(musicSlider))
                    updated = SetNewTarget(new MenuNav(soundSlider).SetConditions(vector.y <= -0.75f));
                else if (_target.Equals(soundSlider))
                    updated = SetNewTarget(new MenuNav(musicSlider).SetConditions(vector.y >= 0.75f));
            }
            if (!updated && oldTarget == null)
                return;
            Image targetImage = oldTarget.gameObject.GetComponentInChildren<Image>();
            if(targetImage != null)
                targetImage.sprite = oldTarget.spriteState.disabledSprite;
            targetImage = _target.GetComponentInChildren<Image>();
            targetImage.sprite = _target.spriteState.highlightedSprite;
        }
    }
    
    
}
