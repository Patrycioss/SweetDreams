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
		[SerializeField] private List<GameObject> _playerPrefabs = new List<GameObject>(4);
		public List<GameObject> players => _playerPrefabs;
		
		[SerializeField] private List<GameObject> _animatedPlayers = new List<GameObject>(4);
		public List<GameObject> animatedPlayers => _animatedPlayers;

		private int[] _ranking = new int[4] {-1, -1, -1, -1};
		public int[] ranking => _ranking;
		public void SetRanking(int[] pRanking)
		{
			_ranking = pRanking;
		}
		
		private static God _instance;
		public static God instance => _instance;
		private void Awake()
		{
			if (_instance is null)
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
			} else Destroy(gameObject);
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
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
	}
}