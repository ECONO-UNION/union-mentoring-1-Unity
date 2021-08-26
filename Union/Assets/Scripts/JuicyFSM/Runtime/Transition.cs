using System.Collections.Generic;
using UnityEngine;

namespace JuicyFSM
{
    public class Transition
    {
        public Condition condition;
        public State nextState;
    }
}
