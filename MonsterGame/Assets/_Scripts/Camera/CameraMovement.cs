using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Players to track.")]
        [SerializeField] private List<GameObject> players;
        [Header("Offset (Taken in calculation)")]
        [SerializeField] private Vector3 offset;
        [Header("Offset (Added raw)")]
        [SerializeField] private Vector3 baseOffset;
        
        void Update()
        {
            Vector3 original = players[0].transform.position;
            float distanceMod = 0.0f;
            for (int i = 1; i < players.Count; i++)
            {
                Vector3 totalVec = (players[i].transform.position - original);
                original += totalVec / 2.0f;
                distanceMod += totalVec.magnitude / 20.0f;
            }
            transform.position = original + (offset * distanceMod + baseOffset);
        }

        void Reset()
        {
            offset = new Vector3(12, 7, 0);
            baseOffset = new Vector3(1, 2, 0);
        }
    }
}
