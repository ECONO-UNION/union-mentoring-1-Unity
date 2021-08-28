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

            public Rect Rect { get => _rect; set => _rect = value; }
            public string ActionName { get => _actionName; internal set => _actionName = value; }

            public Node()
            {
                _rect = new Rect(Screen.width / 4, Screen.height / 4, 170, 30);
            }

            public Node(float xPosition, float yPosition)
            {
                _rect = new Rect(xPosition, yPosition, 170, 30);
            }
        }

        [System.Serializable]
        public class Edge
        {
            [SerializeReference]
            private Node _start;
            [SerializeReference]
            private Node _end;
            [SerializeField]
            private string _conditionName;

            public Node Start { get => _start; internal set => _start = value; }
            public Node End { get => _end; internal set => _end = value; }
            public string ConditionName { get => _conditionName; internal set => _conditionName = value; }

            public Edge(Node start, Node end, string conditionName)
            {
                _start = start;
                _end = end;
                _conditionName = conditionName;
            }
        }

        [HideInInspector]
        [SerializeReference]
        private List<Node> _nodes;
        [HideInInspector]
        [SerializeReference]
        private Node _currentNode;
        [HideInInspector]
        [SerializeReference]
        private Node _startNode;

        [HideInInspector]
        [SerializeReference]
        private List<Edge> _edges;
        [HideInInspector]
        [SerializeReference]
        private Node _edgeNode;
        private string _conditionName;

        public List<Node> Nodes { get => _nodes; }
        public Node CurrentNode { get => _currentNode; }
        public Node StartNode { get => _startNode; }

        public List<Edge> Edges { get => _edges; }
        public Node EdgeNode { get => _edgeNode; }

        public FlowChart()
        {
            _startNode = new Node();
            _nodes = new List<Node>();
            _edges = new List<Edge>();
            _nodes.Add(_startNode);
        }

        #region NODE
        public void ValidateCategories(List<string> actionCategory, List<string> conditionCategory)
        {
            string errorActionName = "<color=red>NOT FOUND</color>";
            foreach (var node in _nodes)
            {
                if (actionCategory.Exists(x => x == node.ActionName) || string.IsNullOrEmpty(node.ActionName))
                    continue;

                node.ActionName = errorActionName;
            }

            foreach (var edge in _edges)
            {
                if (conditionCategory.Exists(x => x == edge.ConditionName) || string.IsNullOrEmpty(edge.ConditionName))
                    continue;

                edge.ConditionName = errorActionName;
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
            CurrentNode.ActionName = name;
        }

        public void AddNode(Vector2 position)
        {
            Node node = new Node(position.x, position.y);
            _nodes.Add(node);
        }

        public void RemoveNode()
        {
            Edges.RemoveAll(x => x.Start == _currentNode || x.End == _currentNode);
            _nodes.Remove(_currentNode);
            _currentNode = null;
        }

        public void ChangeStartNode()
        {
            _startNode = _currentNode;
        }
        #endregion

        #region EDGE
        public void SelectEdgeNode(string conditionName)
        {
            _edgeNode = _currentNode;
            _conditionName = conditionName;
        }

        public void SetEdgeNodeToNull()
        {
            _edgeNode = null;
        }

        public void ConnectEdge()
        {
            Edge edge = new Edge(_edgeNode, _currentNode,_conditionName);
            _edges.Add(edge);
            _edgeNode = null;
        }
    }
    #endregion
}
