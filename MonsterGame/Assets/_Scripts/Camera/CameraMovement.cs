using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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

        private TweenerCore<Vector3, Vector3, VectorOptions> _tweenerCore;

        private void OnEnable()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _tweenerCore = transform.DOMove(new Vector3(0, 0, 0), durationSmoothing);
            _tweenerCore.onComplete = () =>
            {
                _tweenerCore.Restart();
            };
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
            Vector3 origin = new Vector3();
            List<KeyValuePair<Vector3, Vector3>> pos = null;
            if (players.Count > 1)
            {
                List<KeyValuePair<Vector3, Vector3>> keys = new List<KeyValuePair<Vector3, Vector3>>();
                for (int i = 0; i < players.Count; i++)
                {
                    for (int x = 0; x < players.Count; x++)
                    {
                        if(!players[i].transform.position.Equals(players[x].transform.position))
                            keys.Add(new KeyValuePair<Vector3, Vector3>(players[i].transform.position, players[x].transform.position));
                    }
                }
                pos = keys.OrderBy(pair => Vector3.Distance(pair.Key, pair.Value)).ToList();
                pos.Reverse();
                origin = pos[0].Key + pos[0].Value;
                origin /= 2;
            }
            else
            {
                origin = players[0].transform.position;
            }
            _camera.fieldOfView = fov + (players.Count > 1 ? Vector3.Distance(pos[0].Key, pos[0].Value) * fovMultiplier : 10.0f * fovMultiplier);
            _tweenerCore.ChangeValues(transform.position, origin +
                                                          offset * (players.Count > 1
                                                              ? Vector3.Distance(pos[0].Key, pos[0].Value)
                                                              : 1.0f) + baseOffset, durationSmoothing);
        }

        private void Reset()
        {
            offset = new Vector3(0, 0.09f, 0.35f);
            baseOffset = new Vector3(0, 8, 10);
            fov = 40;
            fovMultiplier = 0.5f;
            durationSmoothing = 1.25f;
        }
    }
}
