using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay.Characters
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private State initState;
        [SerializeField] private bool isPlayOnStart;

        private State _currentState;
        private Dictionary<string, State> _states;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _states = new Dictionary<string, State>();
            var states = this.GetComponents<State>().ToList();
            for (int i = 0; i <states.Count; i++)
            {
                var stateType = states[i].GetType();
                states[i].Initialize();
                _states.Add(stateType.Name, states[i]);
            }

            if (isPlayOnStart)
                ChangeState(initState.GetType().Name);
        }

        public void Update()
        {
            if (_currentState == null)
                return;

            _currentState.Tick();
        }

        public void ChangeState(string stateKey)
        {
            if (!_states.ContainsKey(stateKey))
                return;

            var state = _states[stateKey];

            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = state;
            _currentState.Enter();
        }
    }
}