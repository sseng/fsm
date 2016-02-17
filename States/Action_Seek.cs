using UnityEngine;
using FSM;
using VehicleBehavior;

namespace Actions{
	public class Action_Seek: FSMAction{
		public override void execute(FSMContext fsmc, object o){
			Vehicle v = (Vehicle) o;
			Vector3 target = v.steering.GetTarget();
			
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