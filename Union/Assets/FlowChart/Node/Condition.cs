using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Condition : RuntimeNode
    {
        protected abstract bool Check();

        public sealed override void Update()
        {
            if (Check())
            {
                _state = State.Enable;
                foreach (RuntimeNode child in Children)
                {
                    child.Update();
                }
            }
            else
            {
                if (_state == State.Enable)
                {
                    _state = State.Disable;
                    foreach (RuntimeNode child in Children)
                    {
                        child.ChangeToDisableState();
                    }
                }
            }
        }
    }
}