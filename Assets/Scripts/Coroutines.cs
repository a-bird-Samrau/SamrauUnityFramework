using System;
using System.Collections;
using UnityEngine;
using Behaviour = Core.Behaviour;

public class Coroutines : Behaviour
{
    private static Coroutines _instance;

    public static void Initialize()
    {
        if (_instance != null)
        {
            throw new Exception("Обработчик корутинов уже был проинициализирован");
        }

        _instance = Utilities.CreateGameObjectWithBehaviour<Coroutines>("Coroutines");
    }

    public static Coroutine Run(IEnumerator coroutine)
    {
        return _instance.StartCoroutine(coroutine);
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