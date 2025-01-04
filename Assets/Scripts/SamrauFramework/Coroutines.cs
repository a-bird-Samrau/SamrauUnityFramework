using System;
using System.Collections;
using UnityEngine;
using Behaviour = SamrauFramework.Core.Behaviour;

namespace SamrauFramework
{
    public class Coroutines : Core.Behaviour
    {
        private static Coroutines _instance;

        public static void Initialize()
        {
            if (_instance != null)
            {
                throw new Exception("The coroutine handler has already been initialized");
            }

            _instance = Utilities.CreateGameObjectWithBehaviour<Coroutines>("Coroutines");
        }

        public static Coroutine Run(IEnumerator enumerator)
        {
            return _instance.StartCoroutine(enumerator);
        }

        public static void Stop(Coroutine coroutine)
        {
            _instance.StopCoroutine(coroutine);
        }

        public static void StopAll()
        {
            _instance.StopAllCoroutines();
        }
    }
}