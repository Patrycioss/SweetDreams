using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        [Header("Field of View base.")] [SerializeField]
        private float fov;
        [Header("Field of View multiplier")] [SerializeField]
        private float fovMultiplier;

        [Header("Duration of smoothing")] [SerializeField]
        private float durationSmoothing;
        private UnityEngine.Camera _camera;


        private void OnEnable()
        {
            _camera = GetComponent<UnityEngine.Camera>();
        }

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
            List<KeyValuePair<Vector3, Vector3>> keys = new List<KeyValuePair<Vector3, Vector3>>();
            for (int i = 0; i < players.Count; i++)
            {
                for (int x = 0; x < players.Count; x++)
                {
                    if(!players[i].transform.position.Equals(players[x].transform.position))
                        keys.Add(new KeyValuePair<Vector3, Vector3>(players[i].transform.position, players[x].transform.position));
                }
            }
            List<KeyValuePair<Vector3, Vector3>> pos = keys.OrderBy(pair => Vector3.Distance(pair.Key, pair.Value)).ToList();
            pos.Reverse();
            Vector3 origin = pos[0].Key + pos[0].Value;
            origin /= 2;
            _camera.fieldOfView = fov + Vector3.Distance(pos[0].Key, pos[0].Value) * fovMultiplier;
            transform.DOMove(origin + offset * Vector3.Distance(pos[0].Key, pos[0].Value) + baseOffset, durationSmoothing);
            /*Vector3 origin = new Vector3();
            float distanceMod = 0.0f;
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
            
            transform.position = origin + offset * distanceMod + baseOffset;*/
            
        }

        private void Reset()
        {
            offset = new Vector3(12, 7, 0);
            baseOffset = new Vector3(1, 2, 0);
        }
    }
}
