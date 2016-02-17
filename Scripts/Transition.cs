using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace FSM
{
    public class Transition
    {
        public Transition(State target, FSMAction action)
        {
            this.target = target;
            this.action = action;
        }

        private State target;
        private FSMAction action;

        public State Target { get { return target; } }

        public void execute(FSMContext c, GameObject o)
        {
            action.execute(c, o);
        }
    }
}
