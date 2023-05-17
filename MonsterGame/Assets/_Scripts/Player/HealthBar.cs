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
        
        void Start()
        {
            EventBus<PlayerDamagedEvent>.Subscribe(ChangeHealthBar);
        }

        private void OnDisable()
        {
            EventBus<PlayerDamagedEvent>.UnSubscribe(ChangeHealthBar);
        }

        void ChangeHealthBar(PlayerDamagedEvent pEvent)
        {
            Player player = pEvent.Player;
            Image image = transform.GetChild(0).GetComponent<Image>();
            image.sprite = healthBarImages[player.Health];
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = playerToFollow.transform.position;
            transform.position += offset;
        }
    }
}
