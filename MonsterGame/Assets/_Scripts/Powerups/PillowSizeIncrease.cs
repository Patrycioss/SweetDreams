using System;
using UnityEngine;

namespace _Scripts.Powerups
{
    public class PillowSizeIncrease : Powerup.Powerup
    {
        [SerializeField] private float sizeMultiplier;
        private Vector3 _increase;
        
        protected override void Begin()
        {
            GameObject pillow = target.pillow1.gameObject;
            Vector3 scale = pillow.transform.localScale;
            Vector3 localScale = scale;
            _increase = localScale * sizeMultiplier - localScale;
            pillow.transform.localScale += _increase;
        }

        protected override void End()
        {
            target.pillow1.transform.localScale -= _increase;
        }

        protected override void ValuesToCopyToOther(Powerup.Powerup pOther)
        {
            if (!(pOther is PillowSizeIncrease)) return;
            PillowSizeIncrease inc = (PillowSizeIncrease)pOther;
            inc.sizeMultiplier = sizeMultiplier;
        }

        protected override void DisplayEffect() { }
    }
}
