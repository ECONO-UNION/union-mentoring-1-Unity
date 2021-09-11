using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    public class Node : ScriptableObject
    {
        public string Name;
        public string BaseType;
        public string GUID;
        public Vector2 Position;
        public List<Node> Children = new List<Node>();
    }
}