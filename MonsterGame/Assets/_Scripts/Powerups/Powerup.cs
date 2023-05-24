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

        protected Player target;
        
        protected abstract void Power();
        protected abstract void OnPickup();
        protected abstract void End();

        protected IEnumerator EndPowerUp()
        {
            yield return new WaitForSeconds(_duration);
            End();
        }

        protected abstract void DisplayEffect();
        
        private void OnTriggerEnter(Collider other)
        {
            Limb limb = other.GetComponent<Limb>();
            if (limb != null)
            {
                target = limb.player;
                OnPickup();
            }
        }
    }
}
