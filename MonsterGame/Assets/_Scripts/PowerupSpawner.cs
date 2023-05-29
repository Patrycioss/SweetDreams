using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

public class PowerupSpawner : MonoBehaviour
{
	public List<Vector3> spawnPositions = new List<Vector3>();
	
	[FormerlySerializedAs("powerupsPrefabs")] [SerializeField] private List<GameObject> powerupPrefabs = new List<GameObject>();
	
	
	private Dictionary<GameObject, int> _powerupCounts = new Dictionary<GameObject, int>();
	private Dictionary<Vector3, int> _positionCounts = new Dictionary<Vector3, int>();

	public void Start()
	{
		foreach (Vector3 pos in spawnPositions) 
			_positionCounts.Add(pos, 0);
	}

	public void SpawnPowerup()
	{
		int index = GetRandomWeightedIndex(_positionCounts.Values);
		Vector3 position = spawnPositions[index];
		GameObject powerup = powerupPrefabs[GetRandomWeightedIndex(_powerupCounts.Values)];
		Instantiate(powerup, position, Quaternion.identity);
		
		for (int i = 0; i < spawnPositions.Count; i++)
		{
			if (spawnPositions[i] != position)
			{
				_positionCounts[spawnPositions[i]]++;
				break;
			}
		}
		
		for (int i = 0; i < powerupPrefabs.Count; i++)
		{
			if (powerupPrefabs[i] != powerup)
			{
				_powerupCounts[powerupPrefabs[i]]++;
				break;
			}
		}
	}

	public int GetRandomWeightedIndex(ICollection<int> pCounts)
	{
		int total = pCounts.Sum();
		int randomIndex = UnityEngine.Random.Range(0, total);
		int currentIndex = 0;
		foreach (int count in pCounts)
		{
			if (randomIndex < count + currentIndex)
				return currentIndex;
			currentIndex += count;
		}
		return currentIndex;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (Vector3 spawnPosition in spawnPositions)
		{
			Gizmos.DrawSphere(spawnPosition, 0.3f);
		}
	}
}


