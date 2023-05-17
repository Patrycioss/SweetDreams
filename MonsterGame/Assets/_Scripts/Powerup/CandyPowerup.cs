using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Powerup
{
    public abstract class CandyPowerup : MonoBehaviour
    {
        [Header("Duration of Powerup in seconds.")]
        [SerializeField] private float _duration;

        public abstract void power();
        public abstract void pickup(Collider other);
        public abstract void end();

        protected IEnumerator EndPowerUp()
        {
            yield return new WaitForSeconds(_duration);
            end();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.tag.Equals("Player"))
                return;
            pickup(other);
        }
    }
}
