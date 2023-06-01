using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
	public class God : MonoBehaviour
	{
		[SerializeField] private List<GameObject> playerPrefabs = new List<GameObject>();

		private static God _instance;
		public static God Instance => _instance;

		private void Awake()
		{
			if (_instance is null)
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
			} else Destroy(gameObject);
		}

		public static void SwapScene(string pSceneName)
		{
			int buildIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + pSceneName + ".unity");
			if (buildIndex < 0) Debug.LogError("No scene with name " + pSceneName + " found");
	
			SceneManager.LoadSceneAsync(buildIndex);
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private static void OnSceneLoaded(Scene pScene, LoadSceneMode pLoadSceneMode)
		{
			SceneManager.MoveGameObjectToScene(_instance.gameObject, SceneManager.GetSceneByBuildIndex(pScene.buildIndex));
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
	}
}