using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Menu.States
{
    public class MenuStateManager : MonoBehaviour
    {
        private MenuStateManager _instance;
        private static List<CustomState> _states = new();
        private static List<PlayerInput> _inputs = new();
        [CanBeNull]
        private static CustomState FindState(string pName)
        {
            foreach (CustomState state in _states)
            {
                if (state.GetType().Name == pName) 
                    return state;
            }

            return null;
        }
	
        private void Awake()
        {
            if (_instance == null) _instance = this;
            else
            {
                Debug.LogWarning("Can't have more than one MenuStateManager in the scene!");
                Destroy(this);
            }

            _states = GetComponents<CustomState>().ToList();
        }

        private void Start()
        {
            SetState(typeof(MainMenuState));
            Jukebox.instance.PlayRandom();
        }

        [CanBeNull] 
        private static CustomState _currentState;

        public static void SetState(string name)
        {
            SetState(FindState(name));
        }
	
        public static void SetState(Type pStateType) => SetState(FindState(pStateType.Name));
	
        public static void SetState(CustomState pState)
        {
            if (_currentState != null) _currentState.Stop();
		
            _currentState = pState;
		
            if (_currentState != null) _currentState.StateStart();
        }

	
        private void Update()
        {
            if (_currentState != null) _currentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            if (_currentState != null) _currentState.FixedStateUpdate();
        }

        private void OnDisable()
        {
            if (_currentState != null) _currentState.Stop();
        }

        public static void AddPlayer(PlayerInput input)
        {
            _inputs.Add(input);
        }
        
        public static void RemovePlayer(PlayerInput input)
        {
            _inputs.Remove(input);
        }

        public static List<PlayerInput> GetPlayers()
        {
            return _inputs;
        }

        public static void Clear()
        {
            _inputs.Clear();
        }
    }
}
