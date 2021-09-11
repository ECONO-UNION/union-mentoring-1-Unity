using System;
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
            Node node = nodeView.Node;
            Type type = FlowChart.GetNodeType(node.Name);
            var instance = JsonUtility.FromJson(node.Data, type);
            //Debug.Log(instance.GetType());


            //Clear();
            //UnityEngine.Object.DestroyImmediate(editor);
            //editor = Editor.CreateEditor((UnityEngine.Object)instance);
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