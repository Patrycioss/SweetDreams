using System.Collections;
using System.Collections.Generic;
using System.Threading;
using _Scripts.PlayerScripts;
using _Scripts.Ragdoll_Movement;
using UnityEngine;

namespace _Scripts.Powerup
{
    [RequireComponent(typeof(Collider))]
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
                Powerup otherPowerup = (Powerup) target.gameObject.AddComponent(this.GetType());
                otherPowerup.onPlayer = true;
                otherPowerup.target = target;
                otherPowerup._duration = _duration;
                ValuesToCopyToOther(otherPowerup);
                
                otherPowerup.Begin();
                Utils.Timer.StartTimer(_duration, () => { otherPowerup.End(); });
                Destroy(gameObject);
            }
        }
    }
}
