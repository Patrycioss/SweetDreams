using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Attributes;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
	public List<Vector3> spawnPositions = new List<Vector3>();
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (Vector3 spawnPosition in spawnPositions)
		{
			Gizmos.DrawSphere(spawnPosition, 0.3f);
		}
	}
}


