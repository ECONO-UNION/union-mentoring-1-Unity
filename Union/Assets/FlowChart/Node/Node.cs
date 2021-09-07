using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            Running,
            Failure,
            Success,
        }

        // TODO :Hide Inspector
        [SerializeField]
        private string _guid;
        [SerializeField]
        private Vector2 _position;
        [SerializeField]
        private List<Node> _children = new List<Node>();

        private State _state = State.Running;
        private bool _isStarted = false;

        public string Guid { get => _guid; set => _guid = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public List<Node> Children { get => _children; }

        public void Run()
        {
            if (_state != State.Running)
                return;

            if (!_isStarted)
            {
                OnStart();
                _isStarted = true;
            }
            _state = OnUpdate();

            if (_state == State.Failure || _state == State.Success)
            {
                OnStop();
                _isStarted = false;
            }
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
}