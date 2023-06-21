using System;
using System.Net;
using _Scripts.PlayerScripts;
using _Scripts.Ragdoll_Movement;
using UnityEngine;

namespace _Scripts.Pillow
{
    public class Pillow : MonoBehaviour
    {
        [SerializeField] private float _force = 1000;
        [SerializeField] private int _amountTiredApplied = 1;

        private Collider _collider;

        private Limb _thisLimb;
        
        private int a = 0;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _thisLimb = GetComponent<Limb>();
            if (_thisLimb == null) Debug.LogError("Pillow has no limb component!");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_thisLimb.player.sleepiness.tired <= 0) return;
            
            GameObject obj = collision.collider.gameObject;
            if (obj.GetComponent<Pillow>() != null) return;

            Player hitPlayer;
            Limb limb;
            PlayerController playerController = obj.GetComponent<PlayerController>();
            if (playerController != null)
                hitPlayer = playerController.player;
            else
            {
                limb = obj.GetComponent<Limb>();
                if (limb == null) return;
                hitPlayer = limb.player;
            }

            if (hitPlayer.invincible) return;
            if (hitPlayer.sleepiness.tired <= 0) return;

            Rigidbody rb = this.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude < 2.5)
                return;
            
            GameObject pelvis = hitPlayer.controller.gameObject;
            Rigidbody body = pelvis.transform.GetChild(0).GetComponent<Rigidbody>();
            body.AddForce(collision.GetContact(0).normal * -1 * _force * _thisLimb.player.SlapPower);
            hitPlayer.sleepiness.Tire(_amountTiredApplied);
        }
    }
}
