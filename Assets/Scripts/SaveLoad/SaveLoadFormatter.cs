using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SaveLoad.Surrogates;
using UnityEngine;

namespace SaveLoad
{
    public class SaveLoadFormatter
    {
        private readonly BinaryFormatter _formatter;

        public SaveLoadFormatter()
        {
            _formatter = new BinaryFormatter();

            var selector = new SurrogateSelector();

            var vector3SerializationSurrogate = new Vector3SerializationSurrogate();
            var quaternionSerializationSurrogate = new QuaternionSerializationSurrogate();

            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All),
                vector3SerializationSurrogate);

            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All),
                quaternionSerializationSurrogate);

            _formatter.SurrogateSelector = selector;
        }

        public void Serialize(FileStream file, SaveLoadData data)
        {
            _formatter.Serialize(file, data);
        }

        public SaveLoadData Deserialize(FileStream file)
        {
            return (SaveLoadData)_formatter.Deserialize(file);
        }
    }
}