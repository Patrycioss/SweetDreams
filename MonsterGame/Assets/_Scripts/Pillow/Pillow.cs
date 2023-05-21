using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Pillow
{
    public class Pillow : MonoBehaviour
    {
        [SerializeField] private float _force = 1000;
        [SerializeField] private int _amountTiredApplied = 1;
        
        private void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.collider.gameObject;

            bool isPlayer = false;
            
            for (int i = 1; i < 5; i++)
            {
                if (obj.layer == LayerMask.NameToLayer(i.ToString()))
                {
                    if (obj.layer == gameObject.layer) return;
                    isPlayer = true;
                    break;
                }
            }

            if (!isPlayer) return;
            
            Debug.Log("Hit");

            //This is awful xD
            GameObject pelvis = FindPelvis(obj.transform);
            Player player = pelvis.transform.parent.parent.parent.GetComponent<Player>();
            if (player == null) Debug.LogError("Couldn't find player?");
            if (player.invincible) return;
            
            Rigidbody body = pelvis.transform.GetChild(0).GetComponent<Rigidbody>();
            body.AddForce(gameObject.transform.forward * _force);
            player.sleepiness.Tire(_amountTiredApplied);
            // EventBus<PlayerDamagedEvent>.Publish(new PlayerDamagedEvent(player));
        }

        private GameObject FindPelvis(Transform pStart)
        {
            if (pStart.name.Equals("pelvis")) return pStart.gameObject;
            return FindPelvis(pStart.parent);
        }
    }
    
    
}
