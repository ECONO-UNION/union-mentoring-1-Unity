using System;
using System.Collections.Generic;
using UnityEngine;

namespace JuicyFlowChart
{
    [Serializable]
    public class SerializedNode
    {
        public string name;
        public string guid;
        public string type;
        public string data;
        public Vector2 Position;
    }

    public static class SerializedNodeUtil
    {
        public static Dictionary<string, Type> types = new Dictionary<string, Type>();

        public static SerializedNode Serialize(this Node node)
        {
            var result = new SerializedNode()
            {
                type = node.GetType().Name,
                data = JsonUtility.ToJson(node),
                guid = node.GUID
            };
            return result;
        }

        public static Node Deserialize(this SerializedNode serializedNode)
        {
            Type targetType;
            if (!types.TryGetValue(serializedNode.type, out targetType))
            {
                targetType = Type.GetType(serializedNode.type);
                types[serializedNode.type] = targetType;
            }

            return (Node)JsonUtility.FromJson(serializedNode.data, targetType);
        }
    }
}