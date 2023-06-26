using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Menu.States
{
    public class TutorialMenuState : CustomState
    {
        [SerializeField] private GameObject tutorialMenu;
        [SerializeField] private List<FaceOverlayTutorial> _faceOverlayTutorials;
        private int ready, max;

        public override void StateReset()
        {
            
        }

        public override void StateStart()
        {
            EventBus<PlayerReadyUpTutorialEvent>.Subscribe(Adding);
            tutorialMenu.SetActive(true);
            List<PlayerInput> _playerInputs = MenuStateManager.GetPlayers();
            Dictionary<int, int> _chosen = God.instance.ChosenCharacters;
            int index = 0;
            foreach (KeyValuePair<int, int> chosen in _chosen)
            {
                _faceOverlayTutorials[index].SetPlayerInput(_playerInputs[index]);
                _faceOverlayTutorials[index].Initialize(chosen.Value, index);
                index++;
            }
            max = index;
            ready = 0;
            MenuStateManager.Clear();
        }

        public override void StateUpdate()
        {
            
        }

        public override void FixedStateUpdate()
        {
            
        }

        public override void Stop()
        {
            EventBus<PlayerReadyUpTutorialEvent>.UnSubscribe(Adding);
            //tutorialMenu.SetActive(false);
        }

        public void Adding(PlayerReadyUpTutorialEvent pEvent)
        {
            ready += 1;
            if(ready >= max)
                God.instance.SwapScene("GoodPrototype");
        }
    }
}
