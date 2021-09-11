using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFlowChart
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        private Node _node;
        private object _selectedInstance;
        private Type _type;
        private FieldInfo[] _fields;
        private ObjectDrawer _objectDrawer;
        internal void UpdateSelection(NodeView nodeView)
        {
            Clear();
            _node = nodeView.Node;
            _type = FlowChart.GetNodeType(_node.Name);
            _selectedInstance = JsonUtility.FromJson(_node.Data, _type);
            _objectDrawer = new ObjectDrawer();
            _fields = _type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            IMGUIContainer container = new IMGUIContainer(DrawInspectorView);
            Add(container);
        }

        private void DrawInspectorView()
        {
            _objectDrawer.Draw(_selectedInstance, _fields, SaveField);
        }

        private void SaveField()
        {
            _node.Data = JsonUtility.ToJson(_selectedInstance);
        }
    }
}