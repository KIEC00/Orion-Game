using System;
using System.Collections;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    public const string GLOBAL_OBJECT_NAME = "[Coroutines]";

    public static Coroutines Instance { get => _instance != null ? _instance : CreateComponent(); }

    public static Coroutine Run(IEnumerator enumerator) => Instance.StartCoroutine(enumerator);
    public static Coroutine Run(Action action, YieldInstruction yieldInstruction) => Instance.StartCoroutine(ActionRoutine(action, yieldInstruction));
    public static void Stop(IEnumerator enumerator) => Instance.StopCoroutine(enumerator);
    public static void Stop(Coroutine coroutine) => Instance.StopCoroutine(coroutine);

    private static IEnumerator ActionRoutine(Action action, YieldInstruction yieldInstruction)
    {
        yield return yieldInstruction;
        action();
    }

    private static Coroutines _instance = null;
    private static Coroutines CreateComponent()
    {
        var globalObject = new GameObject();
        _instance = globalObject.AddComponent<Coroutines>();
        DontDestroyOnLoad(globalObject);
        return _instance;
    }
}
