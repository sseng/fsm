using UnityEngine;
using System.Collections;
using VehicleBehavior;

namespace VehicleBehavior{	
	public class SteeringBehavior {
		
		private Vehicle vehicle;
		private Vector3 target;
		private Vehicle targetAgent;
		
		private bool seek = false;
		private bool pursuit = false;
		private bool flee = false;
		private bool evade = false;
		private bool wander = false;
		private bool patrol = false;
		
		private float wanderRadius = 5f;
		private float wanderDistance = 0f;
		private float wanderJitter = 5f;
		private Vector3 wanderTarget;
		
		enum Deceleration{ slow = 3, normal = 2, fast = 1 };
		private Deceleration deceleration;
		
		public SteeringBehavior(Vehicle vehicle)
		{
			this.vehicle = vehicle;
			this.target = new Vector3(Vector3.zero);
			this.targetAgent = null;
			deceleration = Deceleration.normal;
			wanderTarget = new Vector3(2.0f, 0.0f, 0.0f);
		}

		void OnGUI(){
			if (GUI.Button(new Rect(10, 10, 90, 20), "Stop all"))
				AllOff();
			if (GUI.Button(new Rect(110, 10, 90, 20), "Seek"))
				seekOn();
			if (GUI.Button(new Rect(210, 10, 90, 20), "pursuit"))
				pursuitOn();
		}
	
		Vector3 Seek(){
			if (seek){
				
				Vector3 desiredVelocity = (targetPos - this.transform.position).normalized * maxSpeed;
				return (desiredVelocity - velocity);
			}
			return new Vector3(Vector3.zero);
		}
		
		Vector3 Arrive()
		{
			if (arrive) 
			{
				Vector3 toTarget = this.target - vehicle.Position();
				float distance = toTarget.magnitude;
				const float decelerationTweaker = 1.0f;
							
				float speed = distance/((float) deceleration * DecelerationTweaker);			
				speed = Mathf.Min(speed, vehicle.MaxSpeed());
				
				Vector3 DesiredVelocity = (toTarget * speed) / distance;
				return (DesiredVelocity - vehicle.Velocity());			
			}
			return new Vector3(Vector3.zero);
		}
		
		private Vector3 Flee()
		{
			if (flee)
			{
				const float panicDistance = 900f;
				if ((vehicle.Position() - target).sqrMagnitude > panicDistance)
				{
					Debug.Log("Square magnitude: " + (vehicle.Position () - target).sqrMagnitude);
					return new Vector3(Vector3.zero);
				}
				Vector3 desiredVelocity = (vehicle.Position() - target).normalized * vehicle.MaxSpeed();
				return (desiredVelocity - vehicle.Velocity ());				
			}
			return new Vector3(Vector3.zero);
		}
		
		Vector3 Pursuit()
		{
			if (pursuit)
			{
				Vector3 toEvader = targetAgent.Position - vehicle.Position();
				
				float RelativeHeading =  Vector3.Dot(vehicle.Heading(), targetAgent.Heading());
				
				if ((Vector3.Dot(toEvader, vehicle.Heading ()) > 0) && (RelativeHeading < -0.95))
				{
					Vector3 seekDesiredVelocity1 = (this.target - vehicle.Position()).normalized * vehicle.MaxSpeed();					
					return (seekDesiredVelocity1 - vehicle.Velocity());
				}
				
				float lookAheadTime = toEvader.magnitude / (vehicle.MaxSpeed() = targetAgent.Velocity().magnitude);
					
				SetTarget(targetAgent.Position() + targetAgent.Velocity() * lookAheadTime);
				Vector3 seekDesiredVelocity2 = (this.target - vehicle.Position()).normalized * Vehicle.MaxSpeed();
				return (seekDesiredVelocity2 - velocity.Velocity());
			}
			return new Vector3(Vector3.zero);
		}
		
		private Vector3 Wander()
		{	
			float RandomClamped = Random.Range(-2.0f, 2.0f);
			
			if (wander)
			{
				wanderTarget += new Vector3(RandomClamped * wanderJitter, 0.0f, RandomClamped * wanderJitter);
				wanderTarget.Normalize();
				wanderTarget *= wanderRadius;
				
				Vector3 targetLocal = wanderTarget + new Vector3(wanderDistance, 0.0f, 0.0f);
				
				Vector3 targetWorld = targetLocal + vehicle.Position();
				setTarget (targetWorld);
				
				Vector3 seekDesiredVelocity = (this.target - vehicle.Position()).normalized * vehicle.MaxSpeed();
				
				return (seekDesiredVelocity - vehicle.Velocity());
			}
			return new Vector3(Vector3.zero);
		}
		
		private Vector3 Patrol()
		{
			if(patrol)
			{
				Vector3 seekDesiredVelocity = (this.target - vehicle.Position()).normalized * vehicle.MaxSpeed();
				return (seekDesiredVelocity - vehicle.Velocity());
			}
			return new Vector3(Vector3.zero);
		}
		
		public void AllOff()
		{
			seek = false;
			arrive = false;
			flee = false;
			pursuit = false;
			evade = false;
			wander = false;
			patrol = false;
		}
		public void SeekOn() { seek = true; }
		public void SeekOff() { seek = false; }
		public void ArriveOn() { arrive = true; }
		public void ArriveOff() { arrive = false; }
		public void FleeOn() { flee = true; }
		public void FleeOff() { flee = false; }
		public void pursuitOn() { pursuit = true; }
		public void pursuitOff() { pursuit = false; }
		public void EvadeOn() { evade = true; }
		public void EvadeOff() { evade = false; }
		public void WanderOn() { wander = true; }
		public void WanderOff() { wander = false; }
		public void PatrolOn() { patrol = true; }
		public void PatrolOff() { patrol = false; }
		
		public Vector2 Calculate()
		{
			return (Seek() + Arrive() + Flee() + Pursuit() + Evade() + Wander() + Patrol());
		}
		
		public GameObject getTarget() { return target; }
		public void setTarget(GameObject t)	{ this.target = t; }
				
		public void SetTarget(Vector3 t) { this.targetPos = t;}
		public void SetTargetAgent(Vehicle targetAgent) { this.targetAgent = targetAgent; }
		public Vehicle GetTargetAgent() { return this.targetAgent; }
		public Vector3 GetTarget() { return target; }
	}
}