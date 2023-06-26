using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using _Scripts.PlayerScripts;
using _Scripts.Ragdoll_Movement;
using UnityEngine;
using Timer = _Scripts.Utils.Timer;

namespace _Scripts.Powerups
{
    public abstract class Powerup : MonoBehaviour
    {
        [Tooltip("Duration of Powerup in seconds.")]
        [SerializeField] private float _duration;
        private bool _onPlayer = false;
        public bool onPlayer
        {
            get { return _onPlayer; }
            set { _onPlayer = value; }
        }

        private int _timerIndex;
        public int timerIndex => _timerIndex;
        
        protected Player target;
        protected abstract void Begin();
        protected abstract void End();
        protected abstract void ValuesToCopyToOther(Powerup pOther);
        protected abstract void DisplayEffect();
        
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (onPlayer) return;
            Limb limb = otherCollider.GetComponent<Limb>();
            if (limb != null)
            {
                target = limb.player;

                Powerup isItThere = target.gameObject.GetComponent<Powerup>();
                
                if (isItThere != null && isItThere.GetType().Equals(this.GetType()))
                {
                    Utils.Timer.ResetTimer(isItThere.timerIndex);
                    Destroy(gameObject);
                    return;
                }
                
                Powerup otherPowerup = (Powerup) target.gameObject.AddComponent(this.GetType());
                otherPowerup.onPlayer = true;
                otherPowerup.target = target;
                otherPowerup._duration = _duration;
                ValuesToCopyToOther(otherPowerup);
                
                otherPowerup.Begin();
                _timerIndex = Utils.Timer.StartBetterTimer(_duration, () =>
                {
                    otherPowerup.End();
                    Destroy(otherPowerup);
                });
                otherPowerup._timerIndex = _timerIndex;
                Destroy(gameObject);
            }
        }
    }
}
