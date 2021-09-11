using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFlowChart
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        private Editor editor;

        internal void UpdateSelection(NodeView nodeView)
        {
            //Clear();

            //UnityEngine.Object.DestroyImmediate(editor);
            //editor = Editor.CreateEditor(nodeView.Node);
            //IMGUIContainer container = new IMGUIContainer(() =>
            //{
            //    if (editor.target)
            //    {
            //        editor.OnInspectorGUI();
            //    }
            //});
            //Add(container);
        }
    }
}