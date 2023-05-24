using System;
using System.Collections;
using UnityEngine;

namespace _Scripts.Utils
{
	public class Timer : MonoBehaviour
	{
		private static Timer instance;

		private void Awake()
		{
			if (instance == null) 
				instance = this;
		}

		public static void StartTimer(float pDurationSeconds, System.Action pOnEnd)
		{
			instance.StartCoroutine(instance.CountDown(pDurationSeconds, pOnEnd));
		}

		private IEnumerator CountDown(float duration, System.Action pCallback)
		{
			yield return new WaitForSeconds(duration);
			pCallback();
		}
	}
}