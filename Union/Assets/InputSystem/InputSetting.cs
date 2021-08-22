using UnityEngine;
using System;
using System.Collections.Generic;

namespace InputSystem
{
    [Serializable]
    public class Key
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private KeyCode _code;

        public string Name { get => _name;}
        public KeyCode Code { get => _code;}
    }

    public class InputSetting : ScriptableObject
    {
        [SerializeField]
        private List<Key> _key;

        public List<Key> Key { get => _key; }
    }
}
