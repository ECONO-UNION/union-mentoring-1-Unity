using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFSM
{
    public static class JuicyFSMEditorPath
    {
        public const string uxmlPath = "Assets/JuicyFSM/Editor/UIBuilder/JuicyFSMEditor.uxml";
        public const string ussPath = "Assets/JuicyFSM/Editor/UIBuilder/JuicyFSMEditor.uss";
    }

    public class JuicyFSMEditor : EditorWindow
    {
        private FlowChartView flowChartView;
        private InspectorView inspectorView;

        [MenuItem("JuicyFSM/Editor ...")]
        public static void OpenWindow()
        {
            JuicyFSMEditor wnd = GetWindow<JuicyFSMEditor>();
            wnd.titleContent = new GUIContent("JuicyFSMEditor");
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
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(JuicyFSMEditorPath.uxmlPath);
            visualTree.CloneTree(root);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(JuicyFSMEditorPath.ussPath);
            root.styleSheets.Add(styleSheet);

            flowChartView = root.Q<FlowChartView>();
            inspectorView = root.Q<InspectorView>();
        }
    }
}