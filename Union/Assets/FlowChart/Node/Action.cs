using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Action : Node
    {
        protected abstract void OnStart();
        protected abstract void OnUpdate();

        public sealed override void Update()
        {
            if (_state == State.Disable)
            {
                _state = State.Enable;
                OnStart();
            }

            OnUpdate();
            foreach (Node child in Children)
            {
                child.Update();
            }
        }
    }
}