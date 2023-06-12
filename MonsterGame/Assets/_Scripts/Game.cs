using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts
{
	[RequireComponent(typeof(SimpleTimer))]
	public class Game : MonoBehaviour
	{
		[SerializeField] private PlayerManager _playerManager;
		[SerializeField] private PowerupSpawner _powerupSpawner;
		[SerializeField] private float _interval;
		
		private SimpleTimer _timer;
		
		
		private int _playersAwake = 0;

		private List<int> _ranking = new List<int>();
		
		private List<int> _ids = new List<int>();

		private void Awake()
		{
			_timer = GetComponent<SimpleTimer>();
		}

		private void Start()
		{
			EventBus<PlayerSleepEvent>.Subscribe(OnPlayerSleep);
			
			Do();
			void Do()
			{
				_powerupSpawner.SpawnPowerup();
				_timer.StartTimer(_interval,Do);
			}
		}

		private void OnPlayerSleep(PlayerSleepEvent pEvent)
		{
			if (_playersAwake == 0)
			{
				_playersAwake = _playerManager.transform.childCount;
				
				foreach (Transform child in _playerManager.transform)
				{
					_ids.Add(child.GetComponent<Player>().id);
				}
			}
			_ranking.Add(pEvent.Player.id);
			_playersAwake--;
			_ids.Remove(pEvent.Player.id);
			if (_playersAwake <= 1)
			{
				_ranking.Add(_ids[0]);
				EndGame();
			}
			Debug.Log(_playersAwake);
			Debug.Log("Player is sleeping");
		}

		private void EndGame()
		{
			Debug.Log("Ending game");
			
			//_ranking.Add(_ids[0]);
			_ranking.Reverse();
			God.instance.SetRanking(_ranking);
			God.instance.SwapScene("FinishScene");
		}

		private void OnDisable()
		{
			EventBus<PlayerSleepEvent>.UnSubscribe(OnPlayerSleep);
		}
	}
}