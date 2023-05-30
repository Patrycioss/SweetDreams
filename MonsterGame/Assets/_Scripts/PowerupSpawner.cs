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
	
	[SerializeField] private List<GameObject> powerupPrefabs = new List<GameObject>();
	
	
	private Dictionary<GameObject, (int index,int count)> _powerupCounts = new Dictionary<GameObject, (int index, int count)>();
	private Dictionary<Vector3, (int index, int count)> _positionCounts = new Dictionary<Vector3, (int index, int count)>();

	public void Start()
	{
		for (int index = 0; index < spawnPositions.Count; index++) 
			_positionCounts.Add(spawnPositions[index], (index, 1));

		for (int index = 0; index < powerupPrefabs.Count; index++) 
			_powerupCounts.Add(powerupPrefabs[index], (index, 1));
	}

	public void SpawnPowerup()
	{
		int posIndex = GetRandomWeightedIndex(_positionCounts.Values);
		Vector3 position = spawnPositions[_positionCounts.Values.ElementAt(posIndex).index];
		
		int powerupIndex = GetRandomWeightedIndex(_powerupCounts.Values);
		GameObject powerup = powerupPrefabs[_powerupCounts.Values.ElementAt(powerupIndex).index];
		Instantiate(powerup, position, Quaternion.identity);
		
		for (int i = 0; i < spawnPositions.Count; i++)
		{
			if (spawnPositions[i] != position)
			{
				(int,int count) pair = _positionCounts[spawnPositions[i]];
				pair.count++;
				_positionCounts[spawnPositions[i]] = pair;
				break;
			}
		}
		
		for (int i = 0; i < powerupPrefabs.Count; i++)
		{
			if (powerupPrefabs[i] != powerup)
			{
				(int,int count) pair = _powerupCounts[powerupPrefabs[i]];
				pair.count++;
				_powerupCounts[powerupPrefabs[i]] = pair;
				break;
			}
		}
	}
	
	public int GetRandomWeightedIndex(ICollection<(int, int count)> pCounts)
	{
		int total = 0;
		foreach ((int, int count) pair in pCounts) 
			total += pair.Item2;

		int randomIndex = UnityEngine.Random.Range(0, total);
		int countIndex = 0;
		
		for (int i = 0; i < pCounts.Count; i++)
		{
			(int, int count) tuple = pCounts.ElementAt(i);
			
			if (randomIndex < tuple.count + countIndex)
				return i;

			countIndex += tuple.count;
		}
		return -1;
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


