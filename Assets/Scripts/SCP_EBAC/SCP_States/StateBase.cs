using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(params object[] o)
        {
            // Debug.Log("OnStateEnter");
        }

        public virtual void OnStateStay(params object[] o)
        {
            // Debug.Log("OnStateStay");
        }

        public virtual void OnStateExit()
        {
            Debug.Log("OnStateExit");
        }
    }    
}
