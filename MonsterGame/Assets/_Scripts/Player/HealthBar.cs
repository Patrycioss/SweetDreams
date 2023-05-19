using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Player playerToFollow;
        [SerializeField] private Vector3 offset;
        [SerializeField] private List<Sprite> healthBarImages;

        private Transform _playerTransform;
        private Vector3 _prevPlayerPosition;

        private void Start()
        {
            if (playerToFollow == null)
                Debug.LogError("Player to follow not assigned to " + name);
            else
            {
                _playerTransform = playerToFollow.transform;
                
                Vector3 playerPosition = _playerTransform.position;
                transform.position = playerPosition;
                transform.position += offset;
                
                _prevPlayerPosition = playerPosition;
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
            Image image = transform.GetChild(0).GetComponent<Image>();
            image.sprite = healthBarImages[player.Health];
        }

        private void Update()
        {
            Vector3 position = playerToFollow.transform.position;
            Vector3 diff = position - _prevPlayerPosition;
            transform.position += new Vector3(diff.x, 0, diff.z);
            _prevPlayerPosition = _playerTransform.position;
        }
    }
}
