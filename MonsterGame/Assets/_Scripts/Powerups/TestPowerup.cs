using UnityEngine;

namespace _Scripts.Powerups
{
	public class TestPowerup : Powerup
	{
		protected override void Begin()
		{
			Debug.Log($"{target} Picked up test");
		}

		protected override void End()
		{
			// throw new System.NotImplementedException();
		}

		protected override void ValuesToCopyToOther(Powerup pOther)
		{
			Debug.Log("Copying");
			// throw new System.NotImplementedException();
		}

		protected override void DisplayEffect()
		{
			// throw new System.NotImplementedException();
		}
	}
}