using System;
using UnityEngine;

namespace _Scripts.Powerup
{
    public class PillowSizeIncrease : Powerup
    {
        private GameObject _makeBigger;

        protected override void Begin()
        {
            _makeBigger.transform.localScale = transform.localScale * 2;
            gameObject.SetActive(false);
            // _makeBigger = pOther.gameObject;
            // Power();
        }

        protected override void End()
        {
            _makeBigger.transform.localScale = transform.localScale / 2;
            Destroy(gameObject);        }

        protected override void ValuesToCopyToOther(Powerup pOther)
        {
            throw new NotImplementedException();
        }

        protected override void DisplayEffect()
        {
            throw new NotImplementedException();
        }
    }
}
