using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Random = UnityEngine.Random;

namespace Editor
{
	[CustomEditor(typeof(PowerupSpawner))]
	public class PowerupSpawnerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			PowerupSpawner spawner = (PowerupSpawner) target;
			
			Vector3 spawnPosition = spawner.transform.position;
			
			if (GUILayout.Button(new GUIContent("New Powerup location")))
			{
				spawner.spawnPositions.Add(
					new Vector3(spawnPosition.x + Random.Range(1.0f,3.0f), spawnPosition.y, spawnPosition.z + Random.Range(1.0f,3.0f))
					);
			};
			
			base.OnInspectorGUI();
		}

		private void OnSceneGUI()
		{
			PowerupSpawner spawner = (PowerupSpawner) target;
			
			for (int i = 0; i < spawner.spawnPositions.Count; i++)
			{
				EditorGUI.BeginChangeCheck();
				Vector3 pos = Handles.PositionHandle(spawner.spawnPositions[i], Quaternion.identity);
				
				if (EditorGUI.EndChangeCheck()) {
					Undo.RecordObject(spawner, "Move spawn position"); 
					EditorUtility.SetDirty(spawner);
					spawner.spawnPositions[i] = pos;
				}
			}
		}
	}
}