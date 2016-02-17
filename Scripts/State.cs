using UnityEngine;
using System;
using System.Collections.Generic;

using System.Text;

namespace FSM
{
    public class State
    {
        public State(FSMAction entryAction, FSMAction updateAction, FSMAction exitAction, string name)
        {
            this.entryAction = entryAction;
			this.updateAction = updateAction;
            this.exitAction = exitAction;
            this.name = name;
            transitions = new Dictionary<string, Transition>();
        } 

        private FSMAction entryAction;
		private FSMAction updateAction;
        private FSMAction exitAction;
        private string name;
        private Dictionary<string, Transition> transitions;

        public string Name { get { return name; } }
		public void update(FSMContext fsmc, GameObject o){
			
			updateAction.execute(fsmc, o);
		}
        public void dispatch(FSMContext fsmc, string eventName, GameObject o)
        {
            if (transitions.ContainsKey(eventName))
            {
				Debug.Log("changing the state by  " + eventName + " in " + name);
                Transition t = (transitions[eventName] as Transition);
				fsmc.CurrentState.exitAction.execute(fsmc, o);
                t.execute(fsmc, o);
                fsmc.CurrentState = t.Target;
                fsmc.CurrentState.entryAction.execute(fsmc, o);
				Debug.LogWarning(" the current state is " + fsmc.CurrentState.name);
            }
        }

        public void addTransition(Transition t, string eventName)
        {
            transitions[eventName] = t;
        }
    }
}
