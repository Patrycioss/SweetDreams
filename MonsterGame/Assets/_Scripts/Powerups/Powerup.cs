using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Powerup
{
    public abstract class Powerup : MonoBehaviour
    {
        [Tooltip("Duration of Powerup in seconds.")]
        [SerializeField] private float _duration;
        
        protected abstract void Power();
        protected abstract void Pickup(Collider pOther);
        protected abstract void End();

        protected IEnumerator EndPowerUp()
        {
            yield return new WaitForSeconds(_duration);
            End();
        }

        protected abstract void DisplayEffect();
        
        private void OnTriggerEnter(Collider other)
        {
            //TODO: BETTER WAY TO DO THIS
            if (!other.tag.Equals("Player"))
                return;
            Pickup(other);
        }
        
        
    }
}
