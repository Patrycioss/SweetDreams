using System;
using System.Collections.Generic;
using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class FaceOverlay : MonoBehaviour
    {
        private Player _player;
        private int _prefab, _characterIndex;
        [SerializeField] private List<Sprite> _blorbos, _beepos, _dingos, _glonks, _spaghettos;
        [SerializeField] private List<Color> _colors;
        private List<Sprite> _chosen;
        private Image _portrait, _circle;

        private void OnEnable()
        {
            _circle = transform.GetChild(0).GetComponent<Image>();
            _portrait = transform.GetChild(1).GetComponent<Image>();
            EventBus<PlayerTiredChangedEvent>.Subscribe(PlayerTired);
        }

        public void Initialize(Player player, int prefab, int characterIndex)
        {
            _player = player;
            _prefab = prefab;
            _characterIndex = characterIndex;
            _circle.color = _colors[_characterIndex];
            switch (_prefab)
            {
                case 0:
                    _chosen = _blorbos;
                    break;
                case 1:
                    _chosen = _beepos;
                    break;
                case 2:
                    _chosen = _dingos;
                    break;
                case 3:
                    _chosen = _glonks;
                    break;
                case 4:
                    _chosen = _spaghettos;
                    break;
            }
            _portrait.sprite = _chosen[0];
        }

        private void PlayerTired(PlayerTiredChangedEvent pEvent)
        {
            if (_player == null || pEvent.Player == null)
                return;
            if (!_player.Equals(pEvent.Player))
                return;
            Sleepiness sleepiness = _player.sleepiness;
            float percentage = (float)sleepiness.tired / sleepiness.maxTired;
            Debug.Log(percentage);
            if (percentage >= 0.51f)
                _portrait.sprite = _chosen[0];
            else if (percentage <= 0.51f && percentage >= 0.01f)
                _portrait.sprite = _chosen[1];
            else
                _portrait.sprite = _chosen[2];
        }
    }
}
