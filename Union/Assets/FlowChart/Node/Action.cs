using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Action : Node
    {
        protected abstract void Act();

        public sealed override void Run()
        {
            _state = State.Enabled;

            Act();
            foreach(Node child in Children)
            {
                child.Run();
            }
        }
    }
}