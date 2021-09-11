using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Task
    {
        public enum State
        {
            Enable,
            Disable
        }

        private List<Task> _children = new List<Task>();
        protected State _state = State.Disable;
        protected GameObject gameObject;
        protected Transform transform;

        public List<Task> Children { get => _children; set => _children = value; }
        public State CurrentState { get => _state; }
        public abstract void Tick();

        internal void ChangeToDisableState()
        {
            _state = State.Disable;
            foreach (Task child in _children)
            {
                child.ChangeToDisableState();
            }
        }

        public Task Clone(GameObject gameObject)
        {
            Task node = MemberwiseClone() as Task;
            node.gameObject = gameObject;
            node.transform = gameObject.transform;
            node.Children = _children.ConvertAll(c => c.Clone(gameObject));
            return node;
        }
    }
}