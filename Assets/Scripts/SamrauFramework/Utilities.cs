using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework
{
    public static class Utilities
    {
        public static T CreateGameObjectWithBehaviour<T>(string name) where T : Behaviour
        {
            return new GameObject(name).AddComponent<T>();
        }
    }
}