using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Menu
{
    public class Scroller : MonoBehaviour
    {
        [Header("The objects that need to be scrolled through.")]
        [SerializeField] private List<GameObject> scrolling;
        [Header("The action map.")]
        [SerializeField] private InputActionAsset _inputActionAsset;
        // Start is called before the first frame update
        void Start()
        {
            EventBus<ScrollLeftEvent>.Subscribe(ScrollLeft);
            EventBus<ScrollRightEvent>.Subscribe(ScrollRight);
        }
        
        void OnDisable()
        {
            EventBus<ScrollLeftEvent>.UnSubscribe(ScrollLeft);
            EventBus<ScrollRightEvent>.UnSubscribe(ScrollRight);
        }

        public void ScrollLeft(ScrollLeftEvent pEvent)
        {
            foreach (GameObject obj in scrolling)
            {
                obj.transform.DOMoveX(obj.transform.position.x - 5, 1);
            }
        }

        public void ScrollRight(ScrollRightEvent pEvent)
        {
            foreach (GameObject obj in scrolling)
            {
                obj.transform.DOMoveX(obj.transform.position.x + 5, 1);
            }
        }
    }
}
