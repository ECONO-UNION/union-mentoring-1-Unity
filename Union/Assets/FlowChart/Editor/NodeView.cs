using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFlowChart
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

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateInputPorts()
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            input.style.flexDirection = FlexDirection.Row;
            input.style.paddingLeft = 12;
            inputContainer.Add(input);
        }

        private void CreateOutputPorts()
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            output.portName = "";
            output.style.flexDirection = FlexDirection.RowReverse;
            output.style.paddingRight = 12;
            outputContainer.Add(output);
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