using UnityEngine;

namespace _Scripts.Menu
{
    public class ButtonStorage : MonoBehaviour
    {
        [SerializeField]
        private Sprite regular, active, triggered;

        public Sprite Regular => regular;

        public Sprite Active => active;

        public Sprite Triggered => triggered;
    }
}
