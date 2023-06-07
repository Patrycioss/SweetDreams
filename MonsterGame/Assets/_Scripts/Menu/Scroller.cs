using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Menu
{
    public class Scroller : MonoBehaviour
    {
        [Header("The objects that need to be scrolled through.")]
        [SerializeField] private List<GameObject> scrolling;
        [Header("Total scrollable objects.")]
        [Header("The action map.")]
        private PlayerInput _playerInput;
        private InputActionMap _map;
        private InputAction _b, _interact, _move;
        private int _currentIndex;
        private bool _busy, _ready, _first;
        private Guid _id;

        void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _map = _playerInput.actions.FindActionMap("Player");
            _interact = _map.FindAction("interaction");
            _b = _map.FindAction("b");
            _move = _map.FindAction("move");
            _b.started += PressB;
            _interact.started += PressA;
        }

        private void OnDisable()
        {
            _map.Disable();
            _b.started -= PressB;
            _interact.started -= PressA;
        }

        void PressB(InputAction.CallbackContext context)
        {
            if (_ready)
            {
                _ready = false;
                EventBus<PlayerReadyDownEvent>.Publish(new PlayerReadyDownEvent(_id, _currentIndex));
            }
            else
            {
                God.instance.SwapScene("UserInterface");
            }
        }

        void PressA(InputAction.CallbackContext context)
        {
            if (!_ready)
            {
                _ready = true;
                Animator anims = scrolling[_currentIndex].GetComponent<Animator>();
                anims.SetTrigger("Select");
                EventBus<PlayerReadyUpEvent>.Publish(new PlayerReadyUpEvent(_id, _currentIndex));
            }
        }

        void Update()
        {
            Vector2 movement = _move.ReadValue<Vector2>();
            if (movement.x >= 0.5f && !_ready && !_busy)
            {
                ScrollRight();
            }
            else if (movement.x <= -0.5f && !_ready && !_busy)
            {
                ScrollLeft();
            }
        }
        
        public void ScrollLeft()
        {
            if (_busy) return;
            _busy = true;
            if (_currentIndex <= 0)
            {
                _currentIndex = scrolling.Count - 1;
                foreach (GameObject obj in scrolling)
                {
                    obj.transform.DOMoveX(obj.transform.position.x - 5 * (scrolling.Count - 1), 1).onComplete += () => { _busy = false; };
                }
            }
            else
            {
                _currentIndex -= 1;
                foreach (GameObject obj in scrolling)
                {
                    obj.transform.DOMoveX(obj.transform.position.x + 5, 1).onComplete += () => { _busy = false; };
                }
            }
        }

        public void ScrollRight()
        {
            if (_busy) return;
            _busy = true;
            if (_currentIndex >= scrolling.Count - 1)
            {
                _currentIndex = 0;
                foreach (GameObject obj in scrolling)
                {
                    obj.transform.DOMoveX(obj.transform.position.x + 5 * (scrolling.Count - 1), 1).onComplete += () =>
                    {
                        _busy = false;
                    };
                } 
            }
            else
            {
                _currentIndex += 1;
                foreach (GameObject obj in scrolling)
                {
                    obj.transform.DOMoveX(obj.transform.position.x - 5, 1).onComplete += () =>
                    {
                        _busy = false;
                    };
                } 
            }
        }

        public Guid ID
        {
            set => _id = value;
            get => _id;
        }

        public bool First
        {
            set => _first = value;
            get => _first;
        }

        public bool Ready
        {
            get => _ready;
        }

        public int CurrentCharacter => _currentIndex;
    }
}
