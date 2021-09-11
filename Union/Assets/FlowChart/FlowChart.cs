using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JuicyFlowChart
{
    [CreateAssetMenu()]
    public class FlowChart : ScriptableObject
    {
        [SerializeField]
        private int _rootID;
        [SerializeField]
        private List<Node> _nodes = new List<Node>();

        public int RootID { get => _rootID; internal set => _rootID = value; }
        public List<Node> Nodes { get => _nodes; internal set => _nodes = value; }

        private static Dictionary<string, Type> _nodeTypes = new Dictionary<string, Type>();
        public static Type GetNodeType(string key)
        {
            Type type;
            if (!_nodeTypes.TryGetValue(key, out type))
            {
                type = Type.GetType(key);
                _nodeTypes[key] = type;
            }
            return type;
        }

        public Node CreateNode(string type, string baseType, Vector2 position)
        {
            Node node = new Node();
            node.Name = type;

            node.BaseType = baseType;
            node.ID = GUID.Generate().GetHashCode();
            node.Position = position;
            if (_rootID == 0)
            {
                SetRootNode(node);
            }

            var instance = Activator.CreateInstance(FlowChart.GetNodeType(type));
            node.Data = JsonUtility.ToJson(instance);

            _nodes.Add(node);
            EditorUtility.SetDirty(this);
            return node;
        }

        public void SetRootNode(Node target)
        {
            if (_rootID == 0)
            {
                _nodes.ForEach((node) =>
                {
                    if (node.ChildrenID.Contains(target.ID))
                    {
                        _nodes.Remove(target);
                    }
                });
            }
            _rootID = target.ID;
            EditorUtility.SetDirty(this);
        }

        public void DeleteNode(Node node)
        {
            if (node.ID == _rootID)
            {
                _rootID = 0;
            }

            _nodes.Remove(node);
            EditorUtility.SetDirty(this);
        }

        public void AddChild(Node parent, Node child)
        {
            parent.ChildrenID.Add(child.ID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(Node parent, Node child)
        {
            parent.ChildrenID.Remove(child.ID);
            EditorUtility.SetDirty(this);
        }

        #region Runtime
        public void Update()
        {
            //if (_rootNode == null)
            //{
            //    Debug.LogWarning("Not found root node");
            //    return;
            //}

            //_rootNode.Update();
        }

        public FlowChart Clone(GameObject gameObject)
        {
            //FlowChart flowChart = Instantiate(this);
            //flowChart.name = string.Format($"{name} (RUNTIME)");
            //flowChart.RootNode = flowChart.RootNode.Clone(gameObject);
            //flowChart.Nodes = new List<Node>();
            //Traverse(flowChart.RootNode, (n) => { flowChart.Nodes.Add(n); });
            //return flowChart;
            return null;
        }

        public void Traverse(Node node, Action<Node> visiter)
        {
            //if (node != null)
            //{
            //    visiter.Invoke(node);
            //    var childrenID = node.ChildrenID;
            //    childrenID.ForEach((n) => Traverse(n, visiter));
            //}
        }
        #endregion
    }
}