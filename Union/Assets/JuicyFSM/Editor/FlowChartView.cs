using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFSM
{
    public class FlowChartView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<FlowChartView, GraphView.UxmlTraits> { }
        private FlowChart _flowChart;

        public FlowChartView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(JuicyFSMEditorPath.ussPath);
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnUndoRedo()
        {
            PopulateView(_flowChart);
            AssetDatabase.SaveAssets();
        }

        internal void PopulateView(FlowChart flowChart)
        {
            _flowChart = flowChart;
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements.ToList());
            graphViewChanged += OnGraphViewChanged;

            DrawNode();
            DrawEdge();
        }

        private void DrawNode()
        {
            _flowChart.Nodes.ForEach(node => CreateNodeView(node));
        }

        private void DrawEdge()
        {
            _flowChart.Nodes.ForEach(node =>
            {
                node.Children.ForEach(c =>
                {
                    NodeView parentView = FindNodeView(node);
                    NodeView childView = FindNodeView(c);

                    Edge edge = parentView.Output.ConnectTo(childView.Input);
                    AddElement(edge);
                });
            });
        }

        private NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.Guid) as NodeView;
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            AddElement(nodeView);
        }

        /// <summary>
        /// �׷��� �� ������ Ư�� �̺�Ʈ(����, �������� ��)�� �߻����� ��� ����Ǵ� �ݹ��Լ�
        /// </summary>
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            // Delete Node
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(element =>
                {
                    NodeView nodeView = element as NodeView;
                    if (nodeView != null)
                    {
                        _flowChart.DeleteNode(nodeView.Node);
                    }

                    Edge edge = element as Edge;
                    if (edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        _flowChart.RemoveChild(parentView.Node, childView.Node);
                    }
                });
            }

            // Create Edge
            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _flowChart.AddChild(parentView.Node, childView.Node);
                });
            }
            return graphViewChange;
        }

        /// <summary>
        /// ���콺 ��Ŭ���� ����Ǵ� �ݹ��Լ�
        /// </summary>
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_flowChart == null)
                return;

            ShowNodeTypes<Action>(evt);
            evt.menu.AppendSeparator();
            ShowNodeTypes<Condition>(evt);
        }

        private void ShowNodeTypes<T>(ContextualMenuPopulateEvent evt) where T : Node
        {
            var types = TypeCache.GetTypesDerivedFrom<T>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) =>
                {
                    var createNodeMethod = typeof(FlowChart).GetMethod("CreateNode").MakeGenericMethod(type);
                    Node node = createNodeMethod.Invoke(_flowChart, null) as Node;
                    CreateNodeView(node);
                });
            }
        }
    }
}