using UnityEngine;
using FSM;
using VehicleBehavior;

namespace Actions{
	public class Action_Patrol: FSMAction{
		public override void execute(FSMContext fsmc, object o){
			Vehicle v = (Vehicle) o;
			Vector3 target = v.steering.GetTarget();
			
			if ((v.Position() - v.waypoint1).magnitude < .01){
				v.steering.SetTarget(v.waypoint2);
			}
			else if ((v.Position() - v.waypoint2).magnitude < .01){
				v.steering.SetTarget(v.waypoint3);
			}
			else if ((v.Position() - v.waypoint3).magnitude < .01){
				v.steering.SetTarget(v.waypoint1);
			}
			
			if ((Vector3.Magnitude(v.Position() - target) < 5) && (!v.steering.ArriveIsOn())){
				v.steering.SeekOff();
				v.steering.ArriveOn();
			}
			
 			if (Vector3.Magnitude(v.Position() - target) < .1){
				v.steering.ArriveOff();
			}
			
		}
	}
}