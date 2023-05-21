using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.PlayerScripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform _transformToFollow;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private List<Sprite> _healthBarImages;
        [SerializeField] private Player _targetPlayer;

        private Vector3 _prevFollowPosition;

        private void Start()
        {
            if (_transformToFollow == null)
                Debug.LogError("Player to follow not assigned to " + name);
            else
            {
                
                Vector3 playerPosition = _transformToFollow.position;
                transform.position = playerPosition;
                transform.position += _offset;
                
                _prevFollowPosition = playerPosition;
            }
            
            
            EventBus<PlayerDamagedEvent>.Subscribe(ChangeHealthBar);
        }

        private void OnDisable()
        {
            EventBus<PlayerDamagedEvent>.UnSubscribe(ChangeHealthBar);
        }

        private void ChangeHealthBar(PlayerDamagedEvent pEvent)
        {
            Player player = pEvent.Player;
            if (!player.Equals(_targetPlayer))
                return;
            Image image = transform.GetChild(0).GetComponent<Image>();
            image.sprite = _healthBarImages[player.health];
        }

        private void Update()
        {
            Vector3 position = _transformToFollow.transform.position;
            Vector3 diff = position - _prevFollowPosition;
            transform.position += new Vector3(diff.x, 0, diff.z);
            _prevFollowPosition = _transformToFollow.position;
        }
    }
}
