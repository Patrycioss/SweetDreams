using System.Collections;
using System.Collections.Generic;
using _Scripts.PlayerScripts;
using _Scripts.Ragdoll_Movement;
using UnityEngine;

namespace _Scripts.Powerup
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

        protected Player target;
        
        protected abstract void Power();
        protected abstract void OnPickup();
        protected abstract void End();
        
        protected abstract void ValuesToCopyToOther(Powerup pOther);
        protected IEnumerator EndPowerUp()
        {
            yield return new WaitForSeconds(_duration);
            Debug.LogError("change");
            End();
        }

        protected abstract void DisplayEffect();
        
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (onPlayer) return;
            Limb limb = otherCollider.GetComponent<Limb>();
            if (limb != null)
            {
                target = limb.player;
                OnPickup();
                Powerup otherPowerup = (Powerup) target.gameObject.AddComponent(this.GetType());
                otherPowerup.onPlayer = true;
                otherPowerup.target = target;
                otherPowerup._duration = _duration;
                ValuesToCopyToOther(otherPowerup);
                Destroy(gameObject);
            }
        }
    }
}
