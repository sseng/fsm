using FSM;
using UnityEngine;
using VehicleBehavior;

namespace Actions{
	public class Action_PursuitEnter: FSMAction{
		public override void execute(FSMContext fsmc, object o){
			Vehicle v = (Vehicle) o;
			v.steering.AllOff();
			v.steering.PursuitOn();
		}
	}
}
