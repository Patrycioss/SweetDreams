using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        public int maxHealth => _maxHealth;

        [FormerlySerializedAs("health")] [SerializeField] private int _health;

        private Vitality _vitality;
        public Vitality vitality => _vitality;

        private PlayerInput _playerInput;

        public int health
        {
            get => _health;
            set => _health = value;
        }

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _vitality = new Vitality(_maxHealth);
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
