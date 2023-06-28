using System;
using System.Collections.Generic;
using _Scripts.Ragdoll_Movement;
using _Scripts.Utils;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(SimpleTimer))]
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        private static int playerCount = 0;

        [SerializeField] private int _id;
        public int id => _id;

        [SerializeField] private float _minAmbientDelay = 5;
        [SerializeField] private float _maxAmbientDelay = 10;
        

        [SerializeField] private float _slapPowerMultiplier;
        [SerializeField] private int _maxSleepiness;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private AnimationMovement _animationMovement;
        [SerializeField] private Pillow.Pillow _pillow1;
        [SerializeField] private Pillow.Pillow _pillow2;
        [SerializeField] private float _invincibilityDuration;
        [SerializeField] private GameObject _head;

        private float _baseVolume = 1;

        [Serializable]
        private class Sound
        {
            public AudioClip clip;
            public float volumeModifier = 1;
        }
        
        [SerializeField] private List<Sound> _ambientSounds = new();
        [SerializeField] private List<Sound> _beingHitSounds = new();
        [SerializeField] private List<Sound> _hitSounds = new();
        [SerializeField] private Sound _sleepSound;

        [SerializeField] private GameObject _sleepVFX;
        [SerializeField] private bool _isFluffo;
        public bool isFluffo => _isFluffo;
        
        
        private AudioSource _audioSource;

        public enum SoundType
        {
            Ambient,
            BeingHit,
            Hit,
            Sleep
        }
        
        public void PlaySound(SoundType pType)
        {
            int index;
            switch (pType)
            {
                case SoundType.Ambient:
                    index = UnityEngine.Random.Range(0, _ambientSounds.Count);
                    _audioSource.clip = _ambientSounds[index].clip;
                    _audioSource.volume = _baseVolume * _ambientSounds[index].volumeModifier;
                    _audioSource.Play();
                    break;
                case SoundType.BeingHit:
                    index = UnityEngine.Random.Range(0, _beingHitSounds.Count);
                    _audioSource.clip = _beingHitSounds[index].clip;
                    _audioSource.volume = _baseVolume * _beingHitSounds[index].volumeModifier;
                    _audioSource.Play();
                    break;
                case SoundType.Hit:
                    index = UnityEngine.Random.Range(0, _hitSounds.Count);
                    _audioSource.clip = _hitSounds[index].clip;
                    _audioSource.volume = _baseVolume * _hitSounds[index].volumeModifier;
                    _audioSource.Play();
                    break;
                case SoundType.Sleep:
                    _audioSource.clip = _sleepSound.clip;
                    _audioSource.volume = _baseVolume * _sleepSound.volumeModifier;
                    _audioSource.Play();
                    break;
            }
        }
        
        
        public GameObject head => _head;
        public PlayerController controller => _controller;
        public AnimationMovement animationMovement => _animationMovement;
        public Pillow.Pillow pillow1 => _pillow1;
        public Pillow.Pillow pillow2 => _pillow2;

        private Sleepiness _sleepiness;
        public Sleepiness sleepiness => _sleepiness;

        private PlayerInput _playerInput;
        private InputActionMap _inputActionMap;
        private InputAction _move;

        private SimpleTimer _timer;

        public bool invincible { get; private set; } = false;

        private int _runningAmbientTimerID = -1;

        private void SetGameLayerRecursive(GameObject pGameObject, int pLayer)
        {
            pGameObject.layer = pLayer;

            for (int i = 0; i < pGameObject.transform.childCount; i++)
            {
                SetGameLayerRecursive(pGameObject.transform.GetChild(i).gameObject, pLayer);
            }
        }
        

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _baseVolume = PlayerPrefs.GetFloat("sound",1.0f);
            
            _timer = GetComponent<SimpleTimer>();
            
            playerCount++;
            
            _playerInput = GetComponent<PlayerInput>();
            _sleepiness = new Sleepiness(this,_maxSleepiness);
            _sleepiness.OnSleep += Disable;
            _sleepiness.OnTiredChanged += OnTiredChanged;

            int layer = LayerMask.NameToLayer(playerCount.ToString());
            SetGameLayerRecursive(gameObject,layer);

            _inputActionMap = _playerInput.actions.FindActionMap("Player");
            if (_inputActionMap == null) Debug.LogError("No action map with name player found in asset!");
            else
            {
                _move = _inputActionMap.FindAction("Move");
                if (_move == null) 
                    Debug.LogError("No action with name Move found in action map: 'Player'");
            }
        }
        
        private void Start()
        {
            EventBus<PlayerBootedEvent>.Publish(new PlayerBootedEvent(this));
            EventBus<PlayerSleepEvent>.Subscribe(OnPlayerSleep);
            AmbientLoop();
        }
        
        private void OnPlayerSleep(PlayerSleepEvent pEvent)
        {
            if (pEvent.Player == this)
            {
                PlaySound(SoundType.Sleep);
                
                if (_sleepVFX != null)
                {
                    GameObject sleepVFX = Instantiate(_sleepVFX, head.transform.position, Quaternion.identity);
                    sleepVFX.AddComponent<FollowTarget>().target = head.transform;
                    Destroy(sleepVFX, 5);
                }

                
                Timer.RemoveTimer(_runningAmbientTimerID);
            }
        }

        private void AmbientLoop()
        {
            PlaySound(SoundType.Ambient);
            _runningAmbientTimerID = Timer.StartBetterTimer(
                UnityEngine.Random.Range(_minAmbientDelay, _maxAmbientDelay), 
                () => AmbientLoop());
        }

        private void OnTiredChanged(int pAmount)
        {
            PlaySound(SoundType.BeingHit);
            invincible = true;
            _timer.StartTimer(_invincibilityDuration, () => invincible = false);
        }

        public void Disable()
        {
            _inputActionMap.Disable();
            _controller.Disable();
        }

        private void OnDisable()
        {
            EventBus<PlayerSleepEvent>.UnSubscribe(OnPlayerSleep);
            _sleepiness.OnSleep -= Disable;
            _sleepiness.OnTiredChanged -= OnTiredChanged;
        }

        private void FixedUpdate()
        {
            Vector2 direction = _move.ReadValue<Vector2>();
            if (direction.magnitude != 0)
            {
                _controller.Move(-direction);
                
                if (!_animationMovement.walkingEnabled) 
                    _animationMovement.Enable(true);
            }
            else if (_animationMovement.walkingEnabled) _animationMovement.Enable(false);
        }

     

        public float SlapPower
        {
            get => _slapPowerMultiplier;
            set => _slapPowerMultiplier = value;
        }


        // void Update()
        // {
        //     // if (!Input.GetKeyDown(KeyCode.A))
        //     //     return;
        //     // health -= 1;
        //     // EventBus<PlayerDamagedEvent>.Publish(new PlayerDamagedEvent(this));
        // }

        public static void ResetPlayerCount()
        {
            playerCount = 0;
        }
    }
}
