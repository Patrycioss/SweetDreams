using UnityEngine;

namespace _Scripts.Menu
{
    public abstract class CustomState : MonoBehaviour
    {
        public abstract void StateReset();
        public abstract void StateStart();
        public abstract void StateUpdate();
        public abstract void FixedStateUpdate();
        public abstract void Stop();
    }
}
