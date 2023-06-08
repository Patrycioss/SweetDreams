using System;
using _Scripts.PlayerScripts;
using _Scripts.Ragdoll_Movement;
using UnityEngine;

namespace _Scripts.Pillow
{
    public class Pillow : MonoBehaviour
    {
        [SerializeField] private float _force = 1000;
        [SerializeField] private int _amountTiredApplied = 1;

        private Limb _limb;        
        private void Start()
        {
            _limb = GetComponent<Limb>();
            if (_limb == null) Debug.LogError("Pillow has no limb component!");
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
                Player self = _limb.player;
                if (self.sleepiness.tired <= 0) return;

                Rigidbody rb = _limb.GetComponent<Rigidbody>();
                if (rb.velocity.magnitude < 2.5)
                    return;
                
                GameObject pelvis = player.controller.gameObject;
                Rigidbody body = pelvis.transform.GetChild(0).GetComponent<Rigidbody>();
                body.AddForce(collision.GetContact(0).normal * _force);
                player.sleepiness.Tire(_amountTiredApplied);
            }
        }
    }
}
