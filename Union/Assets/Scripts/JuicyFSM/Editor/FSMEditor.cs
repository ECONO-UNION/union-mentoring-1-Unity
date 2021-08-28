using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JuicyFSM.Editor
{
    public class FSMEditor : EditorWindow
    {
        private const string _saveFlowChart = "Save Flow Chart";
        private const string _saveComplete = "Save Complete";

        private const string _emptyActionName = "<color=yellow>Empty</color>";
        private const string _emptyMenuName = "Empty";
        private const string _startNodeName = "START";
        private const string _selectAction = "Select Action";
        private const string _changeAction = "Change Action";

        private const string _addNode = "Add Node";
        private const string _deleteNode = "Delete Node";
        private const string _addEdge = "Add Edge";
        private const string _deleteEdge = "Delete Edge";
        private const string _changeStartNode = "Change Start Node";

        private const string _errorNotFound = "Action not found";
        private const string _errorDescription = "User defined Action class could not be found.";
        private const string _errorOK = "OK";

        // Editor
        private FlowChart _currentFlowChart;
        private List<string> _actionCategory = new List<string>();
        private List<string> _conditionCategory = new List<string>();
        private Vector2 _mousePosition;

        // Node
        private Vector2 _scrollPosition;
        private GUIStyle _nodeViewStyle;
        private float _nodeViewSize;
        private int _buttonSize;

        // Style
        private Color _HighlightEditorColor;
        private Color _NormalEditorColor;
        private Color _originEditorColor;
        private GUIStyle _nodeStyle;
        private GUIStyle _edgeStyle;

        [MenuItem("JuicyFSM/Show")]
        private static void ShowEditor()
        {
            GetWindow<FSMEditor>("FSM Editor");
        }

        private void OnEnable()
        {
            _nodeViewStyle = new GUIStyle();
            _nodeViewStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/backgroundwithinnershadow.png") as Texture2D;
            _nodeViewStyle.border = new RectOffset(12, 12, 12, 12);
            _nodeViewStyle.normal.textColor = Color.gray;
            _nodeViewStyle.fontSize = 15;
            _nodeViewStyle.fontStyle = FontStyle.Bold;
            _nodeViewSize = 2000f;
            _buttonSize = 120;

            _HighlightEditorColor = new Color(0, 0.9f, 0.4f);
            _NormalEditorColor = new Color(0.3f, 0.4f, 0.5f);
            _originEditorColor = Color.white;

            _nodeStyle = new GUIStyle();
            _nodeStyle.alignment = TextAnchor.MiddleCenter;
            _nodeStyle.fontStyle = FontStyle.Bold;
            _nodeStyle.normal.textColor = Color.white;
            _nodeStyle.fontSize = 15;

            _edgeStyle = new GUIStyle();
            _edgeStyle.alignment = TextAnchor.MiddleCenter;
            _edgeStyle.normal.textColor = Color.gray;
            _edgeStyle.fontStyle = FontStyle.Bold;
            _edgeStyle.fontSize = 12;

            RefreshCategories();
            if (_currentFlowChart != null)
            {
                _currentFlowChart.ValidateCategories(_actionCategory, _conditionCategory);
            }
        }

        private void RefreshCategories()
        {
            _actionCategory.Clear();
            var actionTypes = Assembly.GetAssembly(typeof(Action)).GetTypes().Where(t => t.IsSubclassOf(typeof(Action)));
            foreach (Type subClass in actionTypes)
            {
                _actionCategory.Add(subClass.Name);
            }

            _conditionCategory.Clear();
            var conditionTypes = Assembly.GetAssembly(typeof(Condition)).GetTypes().Where(t => t.IsSubclassOf(typeof(Condition)));
            foreach (Type subClass in conditionTypes)
            {
                _conditionCategory.Add(subClass.Name);
            }
        }

        private void Update()
        {
            if (Selection.activeObject is FlowChart)
            {
                if (_currentFlowChart == null || _currentFlowChart != Selection.activeObject as FlowChart)
                {
                    _currentFlowChart = Selection.activeObject as FlowChart;
                    _currentFlowChart.ValidateCategories(_actionCategory, _conditionCategory);
                    Repaint();
                }
            }
        }

        private void OnGUI()
        {
            DrawSaveButton();
            BeginScroll();
            {
                DrawBackground();
                UpdateMouseEvent();
                DrawEdges();
                DrawNodes();
            }
            EndScroll();
        }

        private void DrawSaveButton()
        {
            GUI.color = _HighlightEditorColor;
            if (GUILayout.Button(_saveFlowChart, GUILayout.Width(_buttonSize)))
            {
                EditorUtility.SetDirty(_currentFlowChart);
                Debug.Log(_saveComplete);
            }
            GUI.color = _originEditorColor;
        }

        private void BeginScroll()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, (GUILayout.ExpandWidth(true)));
            if (Event.current.button == 2)
            {
                _scrollPosition -= Event.current.delta * 0.5f;
                Repaint();
            }
        }

        private void EndScroll()
        {
            GUILayout.EndScrollView();
        }

        #region NODE VIEW
        private void DrawBackground()
        {
            GUILayoutOption[] options = { GUILayout.Width(_nodeViewSize), GUILayout.Height(_nodeViewSize) };
            EditorGUILayout.LabelField(_currentFlowChart == null ? "" : _currentFlowChart.name, _nodeViewStyle, options);
            DrawGrid(10, 0.3f, Color.black, _nodeViewSize);
            DrawGrid(100, 1f, Color.black, _nodeViewSize);
        }

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor, float size)
        {
            int widthDivs = Mathf.CeilToInt(size / gridSpacing);
            int heightDivs = Mathf.CeilToInt(size / gridSpacing);

            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
            Vector3 newOffset = new Vector3(gridSpacing, gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, size, 0f) + newOffset);
            }

            for (int i = 0; i < heightDivs; i++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * i, 0) + newOffset, new Vector3(size, gridSpacing * i, 0f) + newOffset);
            }

            Handles.color = Color.white;
        }

        private void DrawEdges()
        {
            if (_currentFlowChart == null)
                return;

            foreach (var edge in _currentFlowChart.Edges)
            {
                Rect startNode = edge.Start.Rect;
                Rect endNode = edge.End.Rect;
                Vector3 startPosition = new Vector3(startNode.x + startNode.width / 2, startNode.y + startNode.height / 2, 0);
                Vector3 endPosition = new Vector3(endNode.x + endNode.width / 2, endNode.y + endNode.height / 2, 0);

                Handles.color = Color.white;
                Handles.DrawLine(startPosition, endPosition);

                Rect edgeRect = new Rect((startPosition + endPosition) / 2, Vector2.zero);
                GUI.Label(edgeRect, edge.ConditionName, _edgeStyle);
            }
        }

        private void DrawNodes()
        {
            BeginWindows();
            if (_currentFlowChart == null)
                return;

            int index = 0;
            foreach (var node in _currentFlowChart.Nodes)
            {
                bool isStartNode = node == _currentFlowChart.StartNode;
                if (isStartNode)
                    GUI.color = _HighlightEditorColor;
                else
                    GUI.color = _NormalEditorColor;
                node.Rect = GUILayout.Window(index, node.Rect, DrawNodeWindow, isStartNode ? _startNodeName : "");
                index++;
            }

            GUI.color = _originEditorColor;
            EndWindows();
        }

        private void DrawNodeWindow(int index)
        {
            string actionText = "";
            string buttonText = "";
            if (string.IsNullOrEmpty(_currentFlowChart.Nodes[index].ActionName))
            {
                actionText = _emptyActionName;
                buttonText = _selectAction;
            }
            else
            {
                actionText = _currentFlowChart.Nodes[index].ActionName;
                buttonText = _changeAction;
            }
            GUILayout.Label(actionText, _nodeStyle);
            GUILayout.Space(15);

            if (GUILayout.Button(buttonText))
            {
                if (_actionCategory.Count == 0)
                {
                    EditorUtility.DisplayDialog(_errorNotFound, _errorDescription, _errorOK);
                    return;
                }

                GenericMenu menu = new GenericMenu();
                foreach (var action in _actionCategory)
                {
                    menu.AddItem(new GUIContent(action), false, ChangeNodeName, action);
                }
                menu.AddSeparator("");
                menu.AddItem(new GUIContent(_emptyMenuName), false, ChangeNodeName, "");
                menu.ShowAsContext();
            }
            GUI.DragWindow();
        }

        #endregion

        #region MOUSE EVENT
        private void UpdateMouseEvent()
        {
            if (_currentFlowChart == null)
                return;

            if (Event.current.type == EventType.MouseDown)
            {
                _mousePosition = Event.current.mousePosition;
                _currentFlowChart.SelectCurrentNode(_mousePosition);
                if (Event.current.button == 0)
                {
                    if (_currentFlowChart.EdgeNode == null)
                        return;

                    if (_currentFlowChart.CurrentNode == null || _currentFlowChart.EdgeNode == _currentFlowChart.CurrentNode)
                    {
                        _currentFlowChart.SetEdgeNodeToNull();
                        return;
                    }

                    _currentFlowChart.ConnectEdge();
                }
                else if (Event.current.button == 1)
                {
                    if (_currentFlowChart.EdgeNode != null)
                    {
                        _currentFlowChart.SetEdgeNodeToNull();
                        return;
                    }

                    CreateBackgroundMenuItem();
                }
            }

            if (_currentFlowChart.EdgeNode != null)
            {
                Vector3 startPosition = new Vector3(
                    _currentFlowChart.CurrentNode.Rect.x + _currentFlowChart.CurrentNode.Rect.width / 2,
                    _currentFlowChart.CurrentNode.Rect.y + _currentFlowChart.CurrentNode.Rect.height / 2, 0);
                Handles.DrawLine(startPosition, Event.current.mousePosition);
                Repaint();
            }
        }

        private void CreateBackgroundMenuItem()
        {
            GenericMenu menu = new GenericMenu();
            if (_currentFlowChart.CurrentNode == null)
            {
                menu.AddItem(new GUIContent(_addNode), false, ExecuteMenuAction, _addNode);
            }
            else
            {
                foreach (var condition in _conditionCategory)
                {
                    menu.AddItem(new GUIContent(string.Format($"{_addEdge}/{condition}")), false, SelectEdgeNode, condition);
                }

                if (_currentFlowChart.CurrentNode != _currentFlowChart.StartNode)
                {
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent(_changeStartNode), false, ExecuteMenuAction, _changeStartNode);
                    menu.AddItem(new GUIContent(_deleteNode), false, ExecuteMenuAction, _deleteNode);
                }
            }
            menu.ShowAsContext();
        }
        #endregion

        #region CALL_BACK
        private void ChangeNodeName(object obj)
        {
            string key = obj.ToString();
            if (string.IsNullOrEmpty(key))
            {
                _currentFlowChart.ChangeActionName("");
            }
            else
            {
                _currentFlowChart.ChangeActionName(key);
            }
        }

        private void SelectEdgeNode(object obj)
        {
            string key = obj.ToString();
            _currentFlowChart.SelectEdgeNode(key);
        }

        private void ExecuteMenuAction(object obj)
        {
            string key = obj.ToString();
            if (key == _addNode)
            {
                _currentFlowChart.AddNode(_mousePosition);
            }
            if (key == _deleteNode)
            {
                _currentFlowChart.RemoveNode();
            }
            else if (key == _changeStartNode)
            {
                _currentFlowChart.ChangeStartNode();
            }
            if (key == _deleteEdge)
            {
                // DELETE
            }
        }
        #endregion
    }
}
