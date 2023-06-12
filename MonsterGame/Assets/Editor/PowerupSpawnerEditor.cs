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
		private void OnSceneGUI()
		{
			PowerupSpawner spawner = (PowerupSpawner) target;
			
			for (int i = 0; i < spawner.spawnPositions.Count; i++)
			{
				EditorGUI.BeginChangeCheck();
				Vector3 pos = Handles.PositionHandle(spawner.spawnPositions[i] + spawner.transform.position, Quaternion.identity);
				
				if (EditorGUI.EndChangeCheck()) {
					Undo.RecordObject(spawner, "Move spawn position"); 
					EditorUtility.SetDirty(spawner);
					spawner.spawnPositions[i] = pos;
				}
			}
		}
	}
}