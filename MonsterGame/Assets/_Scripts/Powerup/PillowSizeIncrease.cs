using System;
using UnityEngine;

namespace _Scripts.Powerup
{
    public class PillowSizeIncrease : CandyPowerup
    {
        private GameObject _makeBigger;
        public override void power()
        {
            _makeBigger.transform.localScale = transform.localScale * 2;
            StartCoroutine(EndPowerUp());
            gameObject.SetActive(false);
        }

        public override void pickup(Collider other)
        {
            _makeBigger = other.gameObject;
            power();
        }

        public override void end()
        {
            _makeBigger.transform.localScale = transform.localScale / 2;
            Destroy(gameObject);
        }
    }
}
