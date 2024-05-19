using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionary;
        private StateBase _currState;
        private object obj;

        public StateBase currState
        {
            get { return _currState;}
        }


        public void Init()
        {
            dictionary = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T enumType, StateBase state)
        {
            dictionary.Add(enumType, state);
        }

        public void SwitchState(T state, object o)
        {
            if(_currState != null) _currState.OnStateExit();

            _currState = dictionary[state];
            
            obj = o;
            _currState.OnStateEnter(obj);
        }

        public void Update()
        {
            if(_currState != null) _currState.OnStateStay(obj);
        }
    }
}