using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace JuicyFlowChart
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        private Node _node;
        private Port input;
        private Port output;
        private Action<Node> _onRootNodeSet;

        public Node Node { get => _node; }
        public Port Input { get => input; }
        public Port Output { get => output; }
        public Action<NodeView> OnNodeSelected { get; internal set; }

        public NodeView(Node node, Action<Node> OnRootNodeSet) : base(FlowChartEditorPath.nodeViewUxml)
        {
            _node = node;
            title = _node.name;
            viewDataKey = _node.Guid;
            _onRootNodeSet = OnRootNodeSet;

            InitNodeStyle();

            if (!_node.IsRoot)
                CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();
        }

        private void InitNodeStyle()
        {
            style.left = _node.Position.x;
            style.top = _node.Position.y;
            style.marginTop = 0;
            style.marginBottom = 0;
            style.marginLeft = 0;
            style.marginRight = 0;
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

        private void SetupClasses()
        {
            if (_node.IsRoot)
            {
                AddToClassList("root");
            }
            else if (_node is Action)
            {
                AddToClassList("action");
            }
            else if (_node is Condition)
            {
                AddToClassList("condition");
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(_node, "Behaviour Tree (Set Position)");
            _node.Position = newPos.position;
            EditorUtility.SetDirty(_node);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            if (OnNodeSelected != null)
            {
                OnNodeSelected.Invoke(this);
            }
        }

        public void SortChildren()
        {
            _node.Children.Sort(SortByHorizontalPosition);
        }

        private int SortByHorizontalPosition(Node left, Node right)
        {
            return left.Position.x < right.Position.x ? -1 : 1;
        }

        /// <summary>
        /// ���콺 ��Ŭ���� ����Ǵ� �ݹ��Լ�
        /// </summary>
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (!_node.IsRoot)
            {
                evt.menu.AppendAction($"Set root", (a) =>
                {
                    _onRootNodeSet?.Invoke(_node);
                });
            }
        }

        internal void UpdateState()
        {
            RemoveFromClassList("enable");
            RemoveFromClassList("disable");

            if (Application.isPlaying)
            {
                switch (_node.CurrentState)
                {
                    case Node.State.Enable:
                        AddToClassList("enable");
                        break;
                    case Node.State.Disable:
                        AddToClassList("disable");
                        break;
                }
            }
        }
    }
}