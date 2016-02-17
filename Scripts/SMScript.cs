using UnityEngine;
using System.Collections.Generic;
using System.Text;
using FSM;
using Actions;
using VehicleBehavior;

[RequireComponent (typeof (CharacterController))]

public class SMScript : MonoBehaviour {
	
	private FSMContext fsmc;
	private float timer;
	private Dictionary<string, int> attributes;
	/*
	private State mine;
	private State bank;
	private State sleep;
	private State drink;	
	
	private Transition sleepEnter;
	private Transition drinkEnter;
	private Transition mineEnter;
	private Transition bankEnter;
	*/
	private State idle;
	private State seek;
	private State flee;
	private State pursuit;
	private State evade;
	private State wander;
	private State patrol;
	
	private Transition seekEnter;
	private Transition idleEnter;
	private Transition fleeEnter;
	private Transition pursuitEnter;
	private Transition evadeEnter;
	private Transition wanderEnter;
	private Transition patrolEnter;
	
	//vehicle
	public float mass = 1.0f;
	public float maxSpeed = 2.0f;
	public float madForce = 2.0f;
	public Vehicle target;
	public Vehicle vehicle;
	
	void Start() 
	{	
		//vehicle
		CharacterController controller = GetComponent<CharacterController>();
		vehicle = new Vehicle(mass, maxSpeed, maxForce, controller.transform.position);

		timer = 0.0f;
		attributes = new Dictionary<string, int>();
		
		idle = new State(new Action_IdleEnter(), new Action_Idle(), new Action_IdleExit(), "idle");
		seek = new State(new Action_SeekEnter(), new Action_Seek(), new Action_SeekExit(), "seek");
		flee = new State(new Action_FleeEnter(), new Action_Flee(), new Action_FleeExit(), "flee");
		pursuit = new State(new Action_PursuitEnter(), new Action_Pursuit(), new Action_PursuitExit(), "pursuit");
		evade = new State(new Action_EvadeEnter(), new Action_Evade(), new Action_EvadeExit(), "evade");
		wander = new State(new Action_WanderEnter(), new Action_Wander(), new Action_WanderExit(), "wander");
		patrol = new State(new Action_PatrolEnter(), new Action_Patrol(), new Action_PatrolExit(), "patrol");
		
		seekEnter = new Transition(seek, new Action_None());
		idleEnter = new Transition(idle, new Action_None());
		fleeEnter = new Transition(flee, new Action_None());
		pursuitEnter = new Transition(pursuit, new Action_None());
		evadeEnter = new Transition(evade, new Action_None());
		wanderEnter = new Transition(wander, new Action_None());
		patrolEnter = new Transition(patrol, new Action_None());
		
		idle.addTransition(seekEnter, "seekEnter");
		idle.addTransition(fleeEnter, "fleeEnter");
		idle.addTransition(pursuitEnter, "pursuitEnter");
		idle.addTransition(evadeEnter, "evadeEnter");
		idle.addTransition(wanderEnter, "wanderEnter");
		idle.addTransition(patrolEnter, "patrolEnter");
		
		seek.addTransition(idleEnter, "idleEnter");
		seek.addTransition(fleeEnter, "fleeEnter");
		seek.addTransition(pursuitEnter, "pursuitEnter");
		seek.addTransition(evadeEnter, "evadeEnter");
		seek.addTransition(wanderEnter, "wanderEnter");
		seek.addTransition(patrolEnter, "patrolEnter");
		
		flee.addTransition(idleEnter, "idleEnter");
		flee.addTransition(seekEnter, "seekEnter");
		flee.addTransition(pursuitEnter, "pursuitEnter");
		flee.addTransition(evadeEnter, "evadeEnter");
		flee.addTransition(wanderEnter, "wanderEnter");
		flee.addTransition(patrolEnter, "patrolEnter");
		
		pursuit.addTransition(idleEnter, "idleEnter");
		pursuit.addTransition(seekEnter, "seekEnter");
		pursuit.addTransition(fleeEnter, "fleeEnter");
		pursuit.addTransition(evadeEnter, "evadeEnter");
		pursuit.addTransition(wanderEnter, "wanderEnter");
		patrol.addTransition(patrolEnter, "patrolEnter");
		
		evade.addTransition(idleEnter, "idleEnter");
		evade.addTransition(seekEnter, "seekEnter");
		evade.addTransition(fleeEnter, "fleeEnter");
		evade.addTransition(pursuitEnter, "pursuitEnter");
		evade.addTransition(wanderEnter, "wanderEnter");
		evade.addTransition(patrolEnter, "patrolEnter");
		
		wander.addTransition(idleEnter, "idleEnter");
		wander.addTransition(seekEnter, "seekEnter");
		wander.addTransition(fleeEnter, "fleeEnter");
		wander.addTransition(pursuitEnter, "pursuitEnter");
		wander.addTransition(evadeEnter, "evadeEnter");
		wander.addTransition(patrolEnter, "patrolEnter");
		
		patrol.addTransition(idleEnter, "idleEnter");
		patrol.addTransition(seekEnter, "seekEnter");
		patrol.addTransition(fleeEnter, "fleeEnter");
		patrol.addTransition(pursuitEnter, "pursuitEnter");
		patrol.addTransition(evadeEnter, "evadeEnter");
		patrol.addTransition(wanderEnter, "wanderEnter");
		
		// set the player controller for use by the state machine
		fsmc = FSM.FSM.createFSMInstance(idle, new Idle(), attributes, "hunter");
		fsmc.setController(GetComponent<CharacterController>());
		vehicle.setController(GetComponent<CharacterController());
	}

	// run the current state update action
	void Update () {
		timer += Time.deltaTime;
		
		statem.update(vehicle);
		vehicle.update();
		timer = 0.0f;
	}
	
	// dispatch an externally sourced event to the context
	public void Dispatch (string fsmevent, GameObject o) {
		statem.dispatch(fsmevent, o);
	}
	
	public Vehicle GetVehicle() { return Vehicle; }
	public FSMContext GetFsmc() { return statem; }
	public void reset() { Start(); }
}

