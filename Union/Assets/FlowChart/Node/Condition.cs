using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Condition : Node
    {
        protected abstract bool Check();

        public sealed override void Update()
        {
            if (Check())
            {
                _state = State.Enable;
                foreach (Node child in Children)
                {
                    child.Update();
                }
            }
            else
            {
                if (_state == State.Enable)
                {
                    _state = State.Disable;
                    foreach (Node child in Children)
                    {
                        child.ChangeToDisableState();
                    }
                }
            }
        }
    }
}