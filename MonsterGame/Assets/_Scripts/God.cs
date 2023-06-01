using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
	public class God : MonoBehaviour
	{
		[SerializeField] private List<GameObject> playerPrefabs = new List<GameObject>();

		[CanBeNull] private int[] _whoWon;
		
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