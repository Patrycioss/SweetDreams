using System;
using System.Net;
using _Scripts.PlayerScripts;
using _Scripts.Ragdoll_Movement;
using UnityEngine;
using UnityEngine.VFX;

namespace _Scripts.Pillow
{
    public class Pillow : MonoBehaviour
    {
        [SerializeField] private GameObject _hitVFX;

        [SerializeField] private float _force = 1000;
        [SerializeField] private int _amountTiredApplied = 1;

        private Collider _collider;
        private Limb _thisLimb;
        
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
            _thisLimb.player.PlaySound(Player.SoundType.Hit);


            Rigidbody rb = this.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude < 2.5)
                return;

            
            GameObject pelvis = hitPlayer.controller.gameObject;
            pelvis.GetComponent<Rigidbody>().AddForce(-collision.GetContact(0).normal * _force * _thisLimb.player.SlapPower);
            hitPlayer.sleepiness.Tire(_amountTiredApplied);
            
            
            GameObject hitVFX = Instantiate(_hitVFX, collision.GetContact(0).point, Quaternion.identity);
            hitVFX.transform.forward = collision.GetContact(0).normal;
            hitVFX.transform.parent = collision.collider.transform;
            Destroy(hitVFX, hitVFX.transform.GetChild(0).GetComponent<VisualEffect>().GetFloat("StarLifetime"));
        }
    }
}
