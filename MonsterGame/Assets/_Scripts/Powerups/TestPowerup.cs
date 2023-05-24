using UnityEngine;

namespace _Scripts.Powerup
{
	public class TestPowerup : Powerup
	{
		protected override void Power()
		{
			throw new System.NotImplementedException();
		}

		protected override void OnPickup()
		{
			Debug.Log($"{target} Picked up test");
		}

		protected override void End()
		{
			throw new System.NotImplementedException();
		}

		protected override void DisplayEffect()
		{
			throw new System.NotImplementedException();
		}
	}
}