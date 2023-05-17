using UnityEngine;

namespace _Scripts.Menu
{
    public class FireOffEvent : MonoBehaviour
    {
        public void FireOffLeft()
        {
            EventBus<ScrollLeftEvent>.Publish(new ScrollLeftEvent());
        }
        public void FireOffRight()
        {
            EventBus<ScrollRightEvent>.Publish(new ScrollRightEvent());
        }
    }
}
