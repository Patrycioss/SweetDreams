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
                    isPlayer = true;
                    break;
                }
            }

            if (!isPlayer) return;
            
            Debug.Log("Hit");

            GameObject pelvis = FindPelvis(obj.transform);
            
            Rigidbody body = pelvis.transform.GetChild(0).GetComponent<Rigidbody>();
            body.AddForce(gameObject.transform.forward * _force);
            Player player = pelvis.transform.parent.parent.parent.GetComponent<Player>();
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
