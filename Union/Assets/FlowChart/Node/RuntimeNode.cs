using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class RuntimeNode
    {
        public enum State
        {
            Enable,
            Disable
        }

        private List<RuntimeNode> _children = new List<RuntimeNode>();
        protected State _state = State.Disable;
        protected GameObject gameObject;

        public List<RuntimeNode> Children { get => _children; set => _children = value; }
        public State CurrentState { get => _state; }
        public abstract void Update();

        internal void ChangeToDisableState()
        {
            _state = State.Disable;
            foreach (RuntimeNode child in _children)
            {
                child.ChangeToDisableState();
            }
        }

        public RuntimeNode Clone(GameObject gameObject)
        {
            RuntimeNode node = MemberwiseClone() as RuntimeNode;
            node.gameObject = gameObject;
            node.Children = _children.ConvertAll(c => c.Clone(gameObject));
            return node;
        }
    }
}