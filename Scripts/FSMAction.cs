using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSM
{
    public abstract class FSMAction
    {
        public abstract void execute(FSMContext c, GameObject o);
    }
}
