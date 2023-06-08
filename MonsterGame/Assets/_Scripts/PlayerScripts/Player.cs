using _Scripts.Ragdoll_Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(SimpleTimer))]
    public class Player : MonoBehaviour
    {
        private static int playerCount = 0;

        [SerializeField] private int _id;
        public int id => _id;

        [SerializeField] private float _slapPowerMultiplier;
        [SerializeField] private int _maxSleepiness;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private AnimationMovement _animationMovement;
        [SerializeField] private Pillow.Pillow _pillow1;
        [SerializeField] private Pillow.Pillow _pillow2;
        [SerializeField] private float _invincibilityDuration;
        
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

        private void OnTiredChanged(int pAmount)
        {
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

        private void Start()
        {
            EventBus<PlayerBootedEvent>.Publish(new PlayerBootedEvent(this));
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
    }
}
