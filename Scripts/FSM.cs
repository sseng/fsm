using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace FSM
{
    public class FSM
    {
        public static FSMContext createFSMInstance(State currentState, FSMAction init, string name)
        {
            return new FSMContext(currentState, init, name);
        }
    }
}
