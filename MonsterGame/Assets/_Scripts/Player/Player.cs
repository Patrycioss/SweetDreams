using UnityEngine;

namespace _Scripts
{
    public class Player : MonoBehaviour
    {
        private int _maxHealth;
        [SerializeField] private int health;
        // Start is called before the first frame update
        void Start()
        {
            EventBus<PlayerBootedEvent>.Publish(new PlayerBootedEvent(this));
        }

        // Update is called once per frame
        void Update()
        {
            // if (!Input.GetKeyDown(KeyCode.A))
            //     return;
            // health -= 1;
            // EventBus<PlayerDamagedEvent>.Publish(new PlayerDamagedEvent(this));
        }

        public int Health
        {
            get => health;
            set => health = value;
        }

        public int MaxHealth => _maxHealth;
    }
}
