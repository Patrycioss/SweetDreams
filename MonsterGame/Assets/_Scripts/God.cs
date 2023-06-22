using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Scripts
{
	public class God : MonoBehaviour
	{
		[SerializeField] private List<GameObject> _playerPrefabs = new(4);
		public List<GameObject> players => _playerPrefabs;
		
		[SerializeField] private List<GameObject> _animatedPlayers = new(4);
		public List<GameObject> animatedPlayers => _animatedPlayers;

		private Dictionary<int, int> _chosenCharacters = new();
		public Dictionary<int, int> ChosenCharacters => _chosenCharacters;

		private int[] _ranking = new int[4] {-1, -1, -1, -1};
		public int[] ranking => _ranking;
		public void SetRanking(List<int> pReverseRanking)
		{
			_ranking = new int[4] {-1, -1, -1, -1};
			for (int i = pReverseRanking.Count -1; i >= 0; i--)
			{
				instance.ranking[i] = pReverseRanking[i];
				Debug.Log("Setting ranking to: " + pReverseRanking[i]);
			}
		}
		//D
		private static God _instance;
		public static God instance => _instance;
		private void Awake()
		{
			if (_instance != null)
				return;
			_instance = this;
			DontDestroyOnLoad(gameObject);
	}

		public void SwapScene(string pSceneName)
		{
			int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + pSceneName + ".unity");
			if (buildIndex < 0) Debug.LogError("No scene with name " + pSceneName + " found");
	
			DontDestroyOnLoad(gameObject);
			SceneManager.LoadSceneAsync(buildIndex);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene pScene, LoadSceneMode pLoadSceneMode)
		{
			SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(pScene.buildIndex));
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnApplicationQuit()
		{
			if (_instance != null)
			{
				Destroy(_instance);
				_instance = null;
			}
		}
	}
}