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
        private Node rootNode;
        [SerializeField]
        private List<Node> _nodes = new List<Node>();

        public List<Node> Nodes { get => _nodes; }

        public void Run()
        {
            rootNode.Run();
        }

        public Node CreateNode<T>() where T : Node
        {
            Node node = ScriptableObject.CreateInstance<T>();
            node.name = node.GetType().Name;
            node.Guid = GUID.Generate().ToString();

            Undo.RecordObject(this, "FlowChart (CreateNode)");
            _nodes.Add(node);
            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }
            Undo.RegisterCompleteObjectUndo(node, "FlowChart (CreateNode)");
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(node, "FlowChart (DeleteNode)");
            _nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            Undo.RecordObject(parent, "FlowChart (AddChild)");
            parent.Children.Add(child);
            EditorUtility.SetDirty(parent);
        }

        public void RemoveChild(Node parent, Node child)
        {
            Undo.RecordObject(parent, "FlowChart (RemoveChild)");
            parent.Children.Remove(child);
            EditorUtility.SetDirty(parent);
        }
    }
}