using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Condition : Node
    {
        protected abstract bool Check();

        public sealed override void Run()
        {
            if (Check())
            {
                foreach (Node child in Children)
                {
                    child.Run();
                }
            }
            else
            {
                if (_state == State.Enabled)
                {
                    _state = State.Disabled;
                    foreach (Node child in Children)
                    {
                        child.ChangeToDisableState();
                    }
                }
            }
        }
    }
}