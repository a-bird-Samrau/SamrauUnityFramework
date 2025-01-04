using System;
using System.Collections.Generic;
using UnityEngine;

namespace SamrauFramework.SaveLoad
{
    [Serializable]
    public class SaveLoadData : ISerializationCallbackReceiver
    {
        [SerializeField] private List<string> _keys;
        [SerializeField] private List<object> _values;

        private Dictionary<string, object> _dictionary;

        public SaveLoadData()
        {
            _keys = new List<string>();
            _values = new List<object>();

            _dictionary = new Dictionary<string, object>();
        }

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();

            foreach (var data in _dictionary)
            {
                _keys.Add(data.Key);
                _values.Add(data.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            _dictionary = new Dictionary<string, object>();

            for (var i = 0; i < Mathf.Min(_keys.Count, _values.Count); i++)
            {
                _dictionary.Add(_keys[i], _values[i]);
            }
        }

        public void AddValue<T>(string key, T value)
        {
            _dictionary[key] = value;
        }

        public T GetValue<T>(string key, T valueDefault)
        {
            if (_dictionary.TryGetValue(key, out var value))
            {
                return (T)value;
            }
            
            AddValue(key, valueDefault);

            return valueDefault;
        }
    }
}