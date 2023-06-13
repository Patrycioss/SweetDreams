using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Stage
{
    public class SpawnPositions : MonoBehaviour
    {
        public List<Vector3> spawnPositions = new List<Vector3>();
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            foreach (Vector3 spawnPosition in spawnPositions)
            {
                Gizmos.DrawSphere(spawnPosition + transform.position, 0.3f);
            }
        }
    }
}
