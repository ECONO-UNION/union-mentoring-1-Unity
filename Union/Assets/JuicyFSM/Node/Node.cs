using System.Collections.Generic;
using UnityEngine;

namespace JuicyFSM
{
    public abstract class Node : ScriptableObject
    {
        // TODO :Hide Inspector
        [SerializeField]
        private string _guid;
        [SerializeField]
        private Vector2 _position;
        [SerializeField]
        private List<Node> _children = new List<Node>();

        public string Guid { get => _guid; set => _guid = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public List<Node> Children { get => _children; }
    }
}