using System.Collections.Generic;
using UnityEngine;
using System;

namespace JuicyFlowChart
{
    [Serializable]
    public class Node
    {
        public string Name;
        public string BaseType;
        public int ID;
        public string Data;
        public Vector2 Position;
        public List<int> ChildrenID = new List<int>();
        public Task Task;
    }
}