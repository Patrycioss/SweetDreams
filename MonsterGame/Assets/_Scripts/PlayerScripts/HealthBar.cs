using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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

        private TweenerCore<Quaternion, Vector3, QuaternionOptions> core;
        private float rotationAmount = 6f;
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

            _targetPlayer.sleepiness.OnTiredChanged += ChangeHealthBar;
        }

        private void OnDisable()
        {
            _targetPlayer.sleepiness.OnTiredChanged -= ChangeHealthBar;
        }

        private void ChangeHealthBar(int pAmount)
        {
            core = transform.DORotate(new Vector3(45, 180, rotationAmount), 0.05f, RotateMode.Fast);
            core.onComplete = () =>
            {
                rotationAmount = -rotationAmount;
                if (rotationAmount >= 0.1f)
                    rotationAmount -= 0.5f;
                if (rotationAmount <= -0.1f)
                    rotationAmount += 0.5f;
                core.ChangeValues(core.startValue, new Vector3(45, 180, rotationAmount), 0.05f);
                if (rotationAmount >= 0.1f || rotationAmount <= -0.1f)
                    core.Restart();
                else
                    rotationAmount = 6f;
            };
            Image image = transform.GetChild(0).GetComponent<Image>();
            image.sprite = _healthBarImages[pAmount];
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
