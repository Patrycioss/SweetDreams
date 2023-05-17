using System;
using UnityEngine;

namespace _Scripts.Ragdoll_Movement
{
	public class MassCopy : MonoBehaviour
	{
		[SerializeField] private Transform _target;


		private void Start()
		{
			if (transform.childCount != _target.childCount) Debug.LogError("Child count mismatch!");
			MatchChildren(transform, _target);
		}

		private void MatchChildren(Transform a, Transform b)
		{
			if (transform.childCount != _target.childCount)
			{
				Debug.LogError("Child count mismatch! for " + a.name + " and " + b.name);
			}

			
			for (int i = 0; i < a.childCount; i++)
			{
				Transform childA = a.GetChild(i);
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