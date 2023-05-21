using _Scripts.Ragdoll_Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        private static int playerCount = 0;
        
        [SerializeField] private int _maxSleepiness;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private AnimationMovement _animationMovement;
        public PlayerController controller => _controller;
        public AnimationMovement animationMovement => _animationMovement;

        private Sleepiness _sleepiness;
        public Sleepiness sleepiness => _sleepiness;

        private PlayerInput _playerInput;
        private InputActionMap _inputActionMap;
        private InputAction _move;

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
            playerCount++;
            
            _playerInput = GetComponent<PlayerInput>();
            _sleepiness = new Sleepiness(this,_maxSleepiness);

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
        
        
        // void Update()
        // {
        //     // if (!Input.GetKeyDown(KeyCode.A))
        //     //     return;
        //     // health -= 1;
        //     // EventBus<PlayerDamagedEvent>.Publish(new PlayerDamagedEvent(this));
        // }
    }
}
