using System.Collections.Generic;

namespace JuicyFSM
{
    public class State
    {
        private Action action;
        private List<Transition> transitions;
    }
}