using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFlowChart
{
    public class FlowChartView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<FlowChartView, GraphView.UxmlTraits> { }
        private FlowChart _flowChart;
        private Label _flowChartName;

        public Action<NodeView> OnNodeSelected { get; internal set; }

        public FlowChartView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(FlowChartEditorPath.ussPath);
            styleSheets.Add(styleSheet);
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

        internal void ClearView()
        {
            DeleteElements(graphElements.ToList());
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
            NodeView nodeView = new NodeView(node, SetRootNode);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }

        private void SetRootNode(Node node)
        {
            _flowChart.SetRootNode(node);
            PopulateView(_flowChart);
        }

        /// <summary>
        /// �׷��� �� ������ Ư�� �̺�Ʈ(����, ��������, ����̵� ��)�� �߻����� ��� ����Ǵ� �ݹ��Լ�
        /// </summary>
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (_flowChart == null)
                return graphViewChange;

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

            // Sort Node
            if (graphViewChange.movedElements != null)
            {
                nodes.ForEach((n) =>
                {
                    NodeView view = n as NodeView;
                    view.SortChildren();
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

            evt.menu.AppendSeparator();
            ShowNodeTypes<Action>(evt);
            ShowNodeTypes<Condition>(evt);
        }

        private void ShowNodeTypes<T>(ContextualMenuPopulateEvent evt) where T : Node
        {
            VisualElement contentViewContainer = ElementAt(1);
            Vector3 screenMousePosition = evt.localMousePosition;
            Vector2 worldMousePosition = screenMousePosition - contentViewContainer.transform.position;
            worldMousePosition *= 1 / contentViewContainer.transform.scale.x;

            var types = TypeCache.GetTypesDerivedFrom<T>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Create {type.BaseType.Name}/{type.Name}", (actionEvent) =>
                {
                    var createNodeMethod = typeof(FlowChart).GetMethod("CreateNode").MakeGenericMethod(type);
                    Node node = createNodeMethod.Invoke(_flowChart, new object[] { worldMousePosition }) as Node;
                    CreateNodeView(node);
                });
            }
        }

        /// <summary>
        /// Edge ������ Edge�� ���� �� �ִ� ������ Port List�� ��ȯ���ִ� �Լ�
        /// </summary>
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
        }

        internal void UpdateNodeState()
        {
            nodes.ForEach((n) =>
            {
                NodeView view = n as NodeView;
                view.UpdateState();
            });
        }
    }
}