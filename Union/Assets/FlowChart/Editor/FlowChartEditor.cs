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
    }

    public class FlowChartEditor : EditorWindow
    {
        private FlowChartView _flowChartView;
        private InspectorView _inspectorView;

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
            OnSelectionChange();
        }

        /// <summary>
        /// Unity Asset선택시 실행되는 콜백함수
        /// </summary>
        private void OnSelectionChange()
        {
            FlowChart flowChart = Selection.activeObject as FlowChart;
            if (flowChart)
            {
                _flowChartView?.PopulateView(flowChart);
            }
            else
            {
                if (!Selection.activeGameObject)
                    return;

                FlowChartRunner runner = Selection.activeGameObject.GetComponent<FlowChartRunner>();
                if (runner == null)
                    return;

                flowChart = runner.FlowChart;
            }
        }
    }
}