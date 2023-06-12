using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Players to track.")]
        [SerializeField] private List<GameObject> players;
        [Header("Offset (Taken in calculation)")]
        [SerializeField] private Vector3 offset;
        [Header("Offset (Added raw)")]
        [SerializeField] private Vector3 baseOffset;

        public void AddPlayer(GameObject pPlayer)
        {
            players.Add(pPlayer);
        }
        
        public void RemovePlayer(GameObject pPlayer)
        {
            players.Remove(pPlayer);
        }
        
        private void Update()
        {
            /*Vector3 original = players[0].transform.position;
            float distanceMod = 0.0f;
            for (int i = 1; i < players.Count; i++)
            {
                Vector3 totalVec = (players[i].transform.position - original);
                original += totalVec / 2.0f;
                distanceMod += totalVec.magnitude / 20.0f;
            }
            transform.position = original + (offset * distanceMod + baseOffset);*/
            float distanceMod = 0.0f;
            Vector3 origin = new Vector3();
            for (int i = 0; i < players.Count; i++)
            {
                Vector3 prev = new Vector3();
                if (i - 1 < 0)
                {
                    prev = players[^1].transform.position;
                }
                else
                {
                    prev = players[i - 1].transform.position;
                }
                origin += players[i].transform.position;
                Vector3 totalVec = (players[i].transform.position - prev);
                distanceMod += totalVec.magnitude;
            }
            origin /= players.Count;
            distanceMod /= 4.0f;
            transform.position = origin + offset * distanceMod + baseOffset;
        }

        private void Reset()
        {
            offset = new Vector3(12, 7, 0);
            baseOffset = new Vector3(1, 2, 0);
        }
    }
}
