using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Enabled,
            Disabled
        }

        // TODO :Hide Inspector
        [SerializeField]
        private string _guid;
        [SerializeField]
        private Vector2 _position;
        [SerializeField]
        private List<Node> _children = new List<Node>();
        [SerializeField]
        private bool _isRoot;
        protected State _state = State.Disabled;

        public abstract void Run();

        public string Guid { get => _guid; set => _guid = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public List<Node> Children { get => _children; }
        public bool IsRoot { get => _isRoot; set => _isRoot = value; }
        internal void ChangeToDisableState()
        {
            _state = State.Disabled;
            foreach (Node child in _children)
            {
                child.ChangeToDisableState();
            }
        }
    }
}