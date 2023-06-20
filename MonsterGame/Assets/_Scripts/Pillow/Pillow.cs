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

        private Limb _limb;        
        private void Start()
        {
            _limb = GetComponent<Limb>();
            if (_limb == null) Debug.LogError("Pillow has no limb component!");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Hit");
            if (collision.collider == this.gameObject) return;
            
            GameObject obj = collision.collider.gameObject;
            

            Limb hitLimb = obj.GetComponent<Limb>();
            if (hitLimb != null)
            {
                if (_limb == null) _limb = GetComponent<Limb>();
                Debug.Log("1");

                Player player = hitLimb.player;
                if (hitLimb.gameObject.Equals(player.pillow1.gameObject) || hitLimb.gameObject.Equals(player.pillow2.gameObject)) return;
                Debug.Log("2");

                if (player.invincible) return;
                Player self = _limb.player;
                Debug.Log("3");

                if (self.sleepiness.tired <= 0) return;
                Debug.Log("4");
                if (player.sleepiness.tired <= 0) return;

                Debug.Log("yes yes");
                
                Rigidbody rb = this.GetComponent<Rigidbody>();
                if (rb.velocity.magnitude < 2.5)
                    return;
                else Debug.Log("Didn't make threshold");
                
                GameObject pelvis = player.controller.gameObject;
                Rigidbody body = pelvis.transform.GetChild(0).GetComponent<Rigidbody>();
                body.AddForce(collision.GetContact(0).normal * -1 * _force * self.SlapPower);
                player.sleepiness.Tire(_amountTiredApplied);
            }
        }
    }
}
