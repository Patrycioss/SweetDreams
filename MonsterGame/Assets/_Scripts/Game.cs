using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Camera;
using _Scripts.PlayerScripts;
using _Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
	[RequireComponent(typeof(SimpleTimer))]
	public class Game : MonoBehaviour
	{
		[SerializeField] private PlayerManager _playerManager;
		[SerializeField] private PowerupSpawner _powerupSpawner;
		[SerializeField] private CameraMovement _cameraMovement;
		[SerializeField] private float _interval;

		[SerializeField] private List<GameObject> _stages = new();

		[SerializeField] private float _transitionDuration = 2f;
		[SerializeField] private float _zoomDuration = 2f;
		
		private SimpleTimer _timer;
		private int _playersAwake = 0;

		private List<int> _ranking = new List<int>();
		private List<int> _ids = new List<int>();

		private void Awake()
		{
			_timer = GetComponent<SimpleTimer>();
			
			if (_stages.Count == 0) Debug.LogError("No stages assigned to Game");
		}

		private void Start()
		{
			GameObject stage = _stages[Random.Range(0, _stages.Count)];
			stage.SetActive(true);
			
			EventBus<PlayerSleepEvent>.Subscribe(OnPlayerSleep);
			
			Do();
			void Do()
			{
				_powerupSpawner.SpawnPowerup();
				_timer.StartTimer(_interval,Do);
			}
			
			Jukebox.instance.PlayRandom();
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
			_cameraMovement.RemovePlayer(pEvent.Player.controller.gameObject);
			_playersAwake--;
			_ids.Remove(pEvent.Player.id);
			if (_playersAwake <= 1)
			{
				_ranking.Add(_ids[0]);
				EndGame();
			}
		}

		private void EndGame()
		{
			_ranking.Reverse();
			
			foreach (Transform child in _playerManager.transform)
			{
				Player player = child.GetComponent<Player>();
				if (player != null && player.id == _ranking[0])
				{
					_cameraMovement.SetTween(player.controller.transform.position, _zoomDuration);
					break;
				}
			}
			
			Jukebox.instance.Stop();
			
			Timer.StartTimer(_transitionDuration, () =>
			{
				God.instance.SetRanking(_ranking);
				God.instance.SwapScene("FinishScene");
			});
		}

		private void OnDisable()
		{
			EventBus<PlayerSleepEvent>.UnSubscribe(OnPlayerSleep);
		}
	}
}