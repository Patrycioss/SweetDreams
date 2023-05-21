using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
	public class MassCopy : MonoBehaviour
	{
		[SerializeField] private Transform _target;

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

				if (childA == null || childB == null)
				{
					Debug.Log("Why the fuck?");
					continue;
				}

				if (childA.gameObject.GetComponent<ConfigurableJoint>() != null)
				{
					childA.gameObject.AddComponent<CopyMotion>()._followTarget = childB;
				}

				MatchChildren(childA, childB);
			}
		}
	}
}