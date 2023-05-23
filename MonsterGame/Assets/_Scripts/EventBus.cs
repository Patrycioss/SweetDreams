using System;
using _Scripts.PlayerScripts;
using UnityEngine.Events;

namespace _Scripts
{
    public sealed class EventBus<T> where T : UnityEvent
    {
        public static event Action<T> OnEvent;

        public static void Subscribe(Action<T> handler)
        {
            OnEvent += handler;
        }

        public static void UnSubscribe(Action<T> handler)
        {
            OnEvent -= handler;
        }

        public static void Publish(T pEvent)
        {
            OnEvent?.Invoke(pEvent);
        }
    }

    public abstract class PlayerEvent : UnityEvent
    {
        protected Player _player;

        protected PlayerEvent(Player pPlayer)
        {
            _player = pPlayer;
        }
        
        public Player Player => _player;
    }

    public sealed class PlayerDamagedEvent : PlayerEvent
    {

        public PlayerDamagedEvent(Player pPlayer) : base(pPlayer){}

    }

    public sealed class PlayerBootedEvent : PlayerEvent
    {

        public PlayerBootedEvent(Player pPlayer) : base(pPlayer){}

    }

    public sealed class ScrollLeftEvent : UnityEvent
    {
        public ScrollLeftEvent()
        {
            
        }
    }
    
    public sealed class ScrollRightEvent : UnityEvent
    {
        public ScrollRightEvent()
        {
            
        }
    }
}
