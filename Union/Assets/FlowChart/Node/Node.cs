using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Enable,
            Disable
        }

        [HideInInspector]
        [SerializeField]
        private string _guid;

        [HideInInspector]
        [SerializeField]
        private Vector2 _position;

        [HideInInspector]
        [SerializeField]
        private List<Node> _children = new List<Node>();

        [HideInInspector]
        [SerializeField]
        private bool _isRoot;

        [HideInInspector]
        [SerializeField]
        protected GameObject gameObject;

        protected State _state = State.Disable;

        public abstract void Run();

        public string Guid { get => _guid; set => _guid = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public List<Node> Children { get => _children; set => _children = value; }
        public bool IsRoot { get => _isRoot; set => _isRoot = value; }
        public State CurrentState { get => _state; }

        internal void ChangeToDisableState()
        {
            _state = State.Disable;
            foreach (Node child in _children)
            {
                child.ChangeToDisableState();
            }
        }

        public Node Clone(GameObject gameObject)
        {
            this.gameObject = gameObject;
            Node node = Instantiate(this);
            node.name = name;
            node.Children = _children.ConvertAll(c => c.Clone(gameObject));
            return node;
        }
    }
}