using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFlowChart
{
    public static class FlowChartEditorPath
    {
        public const string uxmlPath = "Assets/FlowChart/Editor/UIBuilder/FlowChartEditor.uxml";
        public const string ussPath = "Assets/FlowChart/Editor/UIBuilder/FlowChartEditor.uss";
        public const string nodeViewUxml = "Assets/FlowChart/Editor/UIBuilder/NodeView.uxml";
    }

    public class FlowChartEditor : EditorWindow
    {
        private FlowChartView _flowChartView;
        private InspectorView _inspectorView;
        private FlowChart _flowChart;
        private Label _flowChartName;

        [MenuItem("FlowChart/Editor...")]
        public static void OpenWindow()
        {
            FlowChartEditor wnd = GetWindow<FlowChartEditor>();
            wnd.titleContent = new GUIContent("FlowChartEditor");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (Selection.activeObject is FlowChart)
            {
                OpenWindow();
                return true;
            }
            return false;
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(FlowChartEditorPath.uxmlPath);
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(FlowChartEditorPath.ussPath);
            root.styleSheets.Add(styleSheet);

            _flowChartView = root.Q<FlowChartView>();
            _inspectorView = root.Q<InspectorView>();
            _flowChartName = _flowChartView.Q<Label>("flowChartName");

            _flowChartView.OnNodeSelected = OnNodeSelectionChanged;
            OnSelectionChange();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }

        private void OnNodeSelectionChanged(NodeView node)
        {
            _inspectorView.UpdateSelection(node);
        }

        /// <summary>
        /// Unity Asset선택시 실행되는 콜백함수
        /// </summary>
        private void OnSelectionChange()
        {
            FlowChart selectedFlowChart = Selection.activeObject as FlowChart;
            if (!selectedFlowChart)
            {
                if (Selection.activeGameObject)
                {
                    FlowChartRunner runner = Selection.activeGameObject.GetComponent<FlowChartRunner>();
                    if (runner && runner.FlowChart != null)
                    {
                        _flowChart = runner.FlowChart;
                    }
                }
            }
            else
            {
                _flowChart = selectedFlowChart;
            }

            if (_flowChart)
            {
                _flowChartView?.PopulateView(_flowChart);
                if (_flowChartName != null)
                    _flowChartName.text = _flowChart.name;
            }
            else
            {
                _flowChartView?.ClearView();
                if (_flowChartName != null)
                    _flowChartName.text = "";
            }
        }

        private void OnProjectChange()
        {
            if (_flowChart)
            {
                if (_flowChartName != null)
                    _flowChartName.text = _flowChart.name;
            }
        }

        private void OnInspectorUpdate()
        {
            _flowChartView?.UpdateNodeState();
        }
    }
}