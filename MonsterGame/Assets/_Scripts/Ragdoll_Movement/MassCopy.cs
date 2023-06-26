using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
	public class MassCopy : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		[SerializeField] private Player _player;

		private void Start()
		{
			MatchChildren(transform, _target);
		}

		private void MatchChildren(Transform a, Transform b)
		{
			for (int i = 0; i < a.childCount; i++)
			{
				Transform childA = a.GetChild(i);
				if (i >= b.childCount) continue; //Debug.LogError("Child count mismatch! for " + a.name + " and " + b.name
				Transform childB = b.GetChild(i);

				if (childA == null || childB == null) continue;

				if (childA.gameObject.GetComponent<ConfigurableJoint>() != null)
				{
					Limb limb = childA.gameObject.AddComponent<Limb>();
					limb.followTarget = childB;
					limb.player = _player;
				}

				MatchChildren(childA, childB);
			}
		}
	}
}