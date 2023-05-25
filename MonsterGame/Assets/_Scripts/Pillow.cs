using System;
using _Scripts.PlayerScripts;
using _Scripts.Ragdoll_Movement;
using UnityEngine;

namespace _Scripts
{
    public class Pillow : MonoBehaviour
    {
        [SerializeField] private float _force = 1000;
        [SerializeField] private int _amountTiredApplied = 1;

        private Limb _limb;
        
        private void Start()
        {
            _limb = GetComponent<Limb>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.collider.gameObject;

            Limb limb = obj.GetComponent<Limb>();
            if (limb != null)
            {
                if (_limb == null) _limb = GetComponent<Limb>();

                Player player = limb.player;
                if (player == _limb.player) return;
                if (player.invincible) return;

                GameObject pelvis = player.controller.gameObject;
                Rigidbody body = pelvis.transform.GetChild(0).GetComponent<Rigidbody>();
                body.AddForce(gameObject.transform.forward * _force);
                player.sleepiness.Tire(_amountTiredApplied);
            }
        }

        private GameObject FindPelvis(Transform pStart)
        {
            if (pStart.name.Equals("pelvis")) return pStart.gameObject;
            return FindPelvis(pStart.parent);
        }
    }
}
