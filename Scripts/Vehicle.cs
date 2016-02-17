using System;
using UnityEngine;
using FSM;
using VehicleBehavior;

namespace VehicleBehavior
{
	public class Vehicle
	{
		private Vector3 velocity;
		private Vector3 position;
		private Vector3 heading;
		private Vector3 side;
		private Vector3 upDirection;		
		private Vector3 acceleration;
		private Vector3 steeringForce;
		
		private float mass;		
		private float maxSpeed;
		private float maxForce;
		
		private CharacterController controller;
				
		public SteeringBehavior steering;
		public Vector3 waypoint1, waypoint2, waypoint3;
		
		
		public Vehicle (float mass, float maxSpeed, float maxForce, Vector3 initialPos)
		{
			this.velocity = new Vector3(Vector3.zero);
			this.position = initialPos;
			this.heading = new Vector3(Vector3.zero);
			this.mass = mass;
			this.maxSpeed = maxSpeed;
			this.maxForce = maxForce;
			steering = new SteeringBehavior(this);
			upDirection = new Vector3(Vector3.zero);
			this.side = Vector3.Cross(heading, upDirection);
			
			this.waypoint1 = new Vector3(0.0f, 0.0f, 5.0f);
			this.waypoint2 = new Vector3(-5.0f, 0.0f,-5.0f);
			this.waypoint3 = new Vector3(5.0f, 0.0f, -5.0f);
			
		}
		void Update()
		{
			Vector3 steeringForce = steering.calculate();
		}
		public Vector3 Position() { return position; }
		public float MaxSpeed() { return maxSpeed; }
		public Vector3 Velocity() { return velocity; }
		public Vector3 Heading() { return heading; }
		
		public void setController(CharacterController controller) { this.controller = controller; }
		
	}
}

