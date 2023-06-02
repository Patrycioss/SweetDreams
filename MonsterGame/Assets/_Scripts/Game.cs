using System;
using System.Collections.Generic;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private PlayerManager _playerManager;
		
		private int _playersAwake = 0;

		private List<int> _ranking = new List<int>();
		
		private List<int> _ids = new List<int>();

		private void Start()
		{
			EventBus<PlayerSleepEvent>.Subscribe(OnPlayerSleep);
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
			if (_playersAwake == 1)
			{
				EndGame();
			}

			Debug.Log(_playersAwake);
			Debug.Log("Player is sleeping");
		}

		private void EndGame()
		{
			Debug.Log("Ending game");
			
			_ranking.Add(_ids[0]);
			God.instance.SetRanking(_ranking);
			God.instance.SwapScene("FinishScene");
		}

		private void OnDisable()
		{
			EventBus<PlayerSleepEvent>.UnSubscribe(OnPlayerSleep);
		}
	}
}