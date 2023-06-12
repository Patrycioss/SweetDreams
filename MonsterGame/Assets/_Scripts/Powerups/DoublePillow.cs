namespace _Scripts.Powerups
{
	public class DoublePillow : Powerup
	{
		protected override void Begin()
		{
			target.pillow2.gameObject.SetActive(true);
		}

		protected override void End()
		{
			target.pillow2.gameObject.SetActive(false);
		}

		protected override void ValuesToCopyToOther(Powerup pOther){}
		protected override void DisplayEffect() {}
	}
}