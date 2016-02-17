using UnityEngine;
using System;
using System.Collections.Generic; 
using System.Text;

namespace FSM
{
    public class FSMContext
    {
	 	private int curr_health=100;
		private int curr_damage=100;
		private int curr_armor=100;
		private int curr_ammo=100;
		
		public int curr_energy=50;
		public int max_energy=100;
		public int curr_gold=0;
		public int target_gold = 5;
		public int bank_gold=0;
		public int thirst = 100;
		
		public float curr_time;
		public float time_interval= 1.0f;
		
		public State CurrentState;
        private string name;
		private CharacterController controller;
		
		public GameObject miner;
		public GameObject mine;		
		public GameObject bank;
		public GameObject bar;
		public GameObject home;
		
        public FSMContext(State currentState, FSMAction init, string name)
        {
            CurrentState = currentState;
            this.name = name;
            init.execute(this, null);
        }        

        public string Name { get { return name; } }

        public void dispatch(string eventName, GameObject o)
        {
			Debug.LogWarning("dispatch  " + eventName);
            CurrentState.dispatch(this, eventName, o);
        }
		public void update(GameObject o)
        {
            CurrentState.update(this,  o);
        }
		public CharacterController getController()
        {
            return controller;
        }
		public void setController(CharacterController cont)
        {
            controller=cont;
        }
 
    }
}