using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class FaceOverlay : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private List<Color> _colors;
        private Image _portrait, _circle;


        private void Start()
        {
            _portrait = GetComponent<Image>();
            _circle = transform.GetChild(0).GetComponent<Image>();
        }
    }
}
