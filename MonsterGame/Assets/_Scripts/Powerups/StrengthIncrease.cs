using UnityEngine;

namespace _Scripts.Powerups
{
    public class StrengthIncrease : Powerup
    {
        [SerializeField] private float strengthInc;
        private float _totalInc;
        
        protected override void Begin()
        {
            _totalInc = target.SlapPower * strengthInc - target.SlapPower;
            target.SlapPower += _totalInc;
        }

        protected override void End()
        {
            target.SlapPower -= _totalInc;
        }

        protected override void ValuesToCopyToOther(Powerup pOther)
        {
            if (!(pOther is StrengthIncrease)) return;
            StrengthIncrease strengthIncrease = (StrengthIncrease)pOther;
            strengthIncrease.strengthInc = strengthInc;
        }

        protected override void DisplayEffect() {}
    }
}
