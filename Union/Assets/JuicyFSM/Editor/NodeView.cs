using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace JuicyFSM
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        private Node _node;
        private Port input;
        private Port output;

        public Node Node { get => _node; }
        public Port Input { get => input; }
        public Port Output { get => output; }

        public NodeView(Node node)
        {
            _node = node;
            title = node.name;
            viewDataKey = node.Guid;

            style.left = node.Position.x;
            style.top = node.Position.y;
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(_node, "Behaviour Tree (Set Position)");
            _node.Position = newPos.position;
            EditorUtility.SetDirty(_node);
        }
    }
}