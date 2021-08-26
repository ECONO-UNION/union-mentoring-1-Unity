using System.Collections.Generic;
using UnityEngine;

namespace JuicyFSM
{
    [CreateAssetMenu(fileName = "NewFlowChart", menuName = "JuicyFSM/FlowChart")]
    public class FlowChart : ScriptableObject
    {
        [System.Serializable]
        public class Node
        {
            [SerializeField]
            private Rect _rect;
            [SerializeField]
            private string _actionName;
            [SerializeField]
            private bool _isStartNode;
            [SerializeField]
            private List<Edge> _edges;

            public Rect Rect { get => _rect; set => _rect = value; }
            public string ActionName { get => _actionName; internal set => _actionName = value; }
            public bool IsStartNode { get => _isStartNode; internal set => _isStartNode = value; }
            public List<Edge> Edges { get => _edges; internal set => _edges = value; }

            public Node()
            {
                _isStartNode = true;
                _rect = new Rect(Screen.width / 4, Screen.height / 4, 170, 30);
            }

            public Node(float xPosition, float yPosition)
            {
                _isStartNode = false;
                _rect = new Rect(xPosition, yPosition, 170, 30);
            }
        }

        [System.Serializable]
        public class Edge
        {
            [SerializeField]
            private string _conditionName;
            [SerializeField]
            private Node _target;

            public string ConditionName { get => _conditionName; internal set => _conditionName = value; }
            public Node Target { get => _target; internal set => _target = value; }
        }

        [HideInInspector]
        [SerializeField]
        private List<Node> _nodes;
        private Node _currentNode;
        public List<Node> Nodes { get => _nodes; }
        public Node CurrentNode { get => _currentNode; }

        public FlowChart()
        {
            Node startState = new Node();
            _nodes = new List<Node>();
            _nodes.Add(startState);
        }

        public void ValidateActionTypes(List<string> actionCategory)
        {
            string errorActionName = "<color=red>NOT FOUND</color>";
            foreach (var node in _nodes)
            {
                if (actionCategory.Exists(x => x == node.ActionName) || string.IsNullOrEmpty(node.ActionName))
                    continue;

                node.ActionName = errorActionName;
            }
        }

        public void SelectCurrentNode(Vector2 position)
        {
            foreach (var node in _nodes)
            {
                if (node.Rect.Contains(position))
                {
                    _currentNode = node;
                    return;
                }
            }
            _currentNode = null;
        }

        public void ChangeActionName(string name)
        {
            _currentNode.ActionName = name;
        }

        public void AddNode(Vector2 position)
        {
            Node node = new Node(position.x, position.y);
            _nodes.Add(node);
        }

        public void DeleteNode()
        {
            _nodes.Remove(_currentNode);
            _currentNode = null;
        }

        public void ChangeStartNode()
        {
            Node prevStartNode = _nodes.Find(x => x.IsStartNode);
            prevStartNode.IsStartNode = false;
            _currentNode.IsStartNode = true;
        }
    }
}
