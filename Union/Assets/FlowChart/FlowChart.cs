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
        private Node _rootNode;
        [SerializeField]
        private List<Node> _nodes = new List<Node>();

        public List<Node> Nodes { get => _nodes; internal set => _nodes = value; }
        public Node RootNode { get => _rootNode; internal set => _rootNode = value; }

        public Node CreateNode(string name, string baseType, Vector2 position)
        {
            Node node = ScriptableObject.CreateInstance<Node>();
            node.Name = name;
            node.name = name;
            node.BaseType = baseType;
            node.GUID = GUID.Generate().ToString();
            node.Position = position;
            if (_rootNode == null)
            {
                SetRootNode(node);
            }

            _nodes.Add(node);
            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            AssetDatabase.SaveAssets();
            return node;
        }

        public void SetRootNode(Node target)
        {
            if (_rootNode != null)
            {
                _nodes.ForEach((node) =>
                {
                    if (node.Children.Contains(target))
                    {
                        node.Children.Remove(target);
                    }
                });
            }
            _rootNode = target;
        }

        public void DeleteNode(Node node)
        {
            if (node.GUID == _rootNode.GUID)
            {
                _rootNode = null;
            }

            _nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            parent.Children.Add(child);
            EditorUtility.SetDirty(parent);
        }

        public void RemoveChild(Node parent, Node child)
        {
            parent.Children.Remove(child);
            EditorUtility.SetDirty(parent);
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
            if (node)
            {
                visiter.Invoke(node);
                var children = node.Children;
                children.ForEach((n) => Traverse(n, visiter));
            }
        }
        #endregion
    }
}