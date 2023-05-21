using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Pillow
{
    public class PillowCollider : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.collider.gameObject;
            if (!obj.tag.Equals("Body"))
                return;
            if (!obj.name.Equals("pelvis"))
                return;
            Rigidbody body = obj.transform.GetChild(0).GetComponent<Rigidbody>();
            body.AddForce(gameObject.transform.forward * 20000);
            Player player = obj.GetComponent<Player>();
            player.sleepiness.Tire(1);
            EventBus<PlayerDamagedEvent>.Publish(new PlayerDamagedEvent(player));
        }
    }
}
