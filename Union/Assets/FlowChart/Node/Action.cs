using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Action : RuntimeNode
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
            foreach (RuntimeNode child in Children)
            {
                child.Update();
            }
        }
    }
}