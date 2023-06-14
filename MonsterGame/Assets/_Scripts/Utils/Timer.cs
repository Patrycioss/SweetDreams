using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace _Scripts.Utils
{
	public class Timer : MonoBehaviour
	{
		private static Timer instance;
		
		private static Dictionary<int,Pausable> _pausableTimers = new Dictionary<int, Pausable>();

		private void Awake()
		{
			if (instance == null) 
				instance = this;
		}

		private void Update()
		{
			for (int i = _pausableTimers.Count - 1; i >= 0; i--)
			{
				_pausableTimers.ElementAt(i).Value.Update();
			}
		}

		public static void StartTimer(float pDurationSeconds, System.Action pOnEnd)
		{
			instance.StartCoroutine(instance.CountDown(pDurationSeconds, pOnEnd));
		}

		public static void AddTime(int pTimerIndex, float pAmount)
		{
			_pausableTimers[pTimerIndex].AddTime(pAmount);
		}

		public static void ResetTimer(int pTimerIndex)
		{
			_pausableTimers[pTimerIndex].Reset();
		}

		public static int StartBetterTimer(float pDurationSeconds, System.Action pOnEnd)
		{
			Pausable pausable = new();
			int id = pausable.GetHashCode();

			pausable.Start(pDurationSeconds, () =>
			{
				_pausableTimers.Remove(id);
				pOnEnd();
			});
			_pausableTimers.Add(id, pausable);
			return id;
		}

		[CanBeNull]
		public Pausable GetTimer(int pTimerId)
		{
			return _pausableTimers[pTimerId];
		}

		public static int StartLoopingTimer(float pDurationSeconds, System.Action pOnEnd)
		{
			Pausable pausable = new Pausable();
			pausable.Start(pDurationSeconds, pOnEnd);
			void StartTimer()
			{
				pOnEnd();
				pausable.Start(pDurationSeconds, pOnEnd);
			}
			
			int id = pausable.GetHashCode();
			_pausableTimers.Add(id, pausable);
			return id;
		}
		
		public static bool RemoveTimer(int pTimerId)
		{
			return _pausableTimers.Remove(pTimerId);
		}

		private IEnumerator CountDown(float duration, System.Action pCallback)
		{
			yield return new WaitForSeconds(duration);
			pCallback();
		}

		public class Pausable
		{
			public bool paused;
			private float _timePassed;
			private float _duration;
			private Action _callback;

			public void Start(float pDurationInSeconds, Action pCallback)
			{
				paused = false;
				_timePassed = 0;
				_duration = pDurationInSeconds;
				_callback = pCallback;
			}

			public void Stop()
			{
				_timePassed = 0;
				_callback = null;
			}

			public void AddTime(float pAmount)
			{
				_duration += pAmount;
			}
		
			public void Update()
			{
				if (paused) return;
				_timePassed += Time.deltaTime;

				if (_timePassed >= _duration)
				{
					if (_callback != null) _callback();
				}
			}

			public void Reset()
			{
				_timePassed = 0;
			}
		}
	}
	
	
}