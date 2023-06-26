using DG.Tweening;
using UnityEngine;

namespace _Scripts.Powerups
{
	public class HeadSizeIncrease : Powerup
	{
		[SerializeField] private float _increaseAmount;

		private Vector3 _scale;
		
		protected override void Begin()
		{
			_scale = Vector3.one;
			target.head.transform.DOScale(Vector3.one * _increaseAmount, 1f);
		}

		protected override void End() 
		{
			target.head.transform.DOScale(_scale, 1f);
		}

		protected override void ValuesToCopyToOther(Powerup pOther)
		{
			if (!(pOther is HeadSizeIncrease)) return;
			HeadSizeIncrease other = (HeadSizeIncrease)pOther;
			other._increaseAmount = _increaseAmount;
		}

		protected override void DisplayEffect()
		{
		}
	}
}