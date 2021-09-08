using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Action : Node
    {
        protected abstract void OnStart();
        protected abstract void OnUpdate();

        public sealed override void Run()
        {
            if (_state == State.Disabled)
            {
                _state = State.Enabled;
                OnStart();
            }

            OnUpdate();
            foreach (Node child in Children)
            {
                child.Run();
            }
        }
    }
}