using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Powerups
{
	public class PowerupSpawner : MonoBehaviour
	{
		[SerializeField] private Vector3 _offset = new Vector3(0, 1, 0);
		
		public List<Vector3> spawnPositions = new List<Vector3>();
		
		private List<Vector3> _editedSpawnPositions = new List<Vector3>();

		[SerializeField] private List<GameObject> powerupPrefabs = new List<GameObject>();

		private Dictionary<Vector3, bool> _occupied = new Dictionary<Vector3, bool>();

		public void Awake()
		{
			_editedSpawnPositions = spawnPositions.Select(pos => pos + _offset).ToList();

			for (int index = 0; index < _editedSpawnPositions.Count; index++) 
				_occupied.Add(_editedSpawnPositions[index], false);
			
		}

		public bool IsOccupied(Vector3 pPosition)
		{
			return _occupied[pPosition];
		}
		
		public void FreePosition(Vector3 pPosition)
		{
			_occupied[pPosition] = false;
		}

		public void SpawnPowerup()
		{
			List<Vector3> freePositions = new List<Vector3>();
			foreach (KeyValuePair<Vector3, bool> pair in _occupied)
				if (!pair.Value)
					freePositions.Add(pair.Key);

			if (freePositions.Count == 0) return;
			int posIndex = UnityEngine.Random.Range(0, freePositions.Count);
			Vector3 position = freePositions[posIndex];

			_occupied[position] = true;
		
			int powerupIndex = UnityEngine.Random.Range(0, powerupPrefabs.Count);
			if (powerupIndex == -1) return;
			GameObject powerup = powerupPrefabs[powerupIndex];
			Instantiate(powerup, position, Quaternion.identity).GetComponent<Powerup>().spawner = this;
		}
		
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			foreach (Vector3 spawnPosition in spawnPositions)
			{
				Gizmos.DrawSphere(transform.position + spawnPosition, 0.3f);
			}
		}
	}
}


