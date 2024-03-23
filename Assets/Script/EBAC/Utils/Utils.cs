using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
    #if UNITY_EDITOR
    [UnityEditor.MenuItem("Utils/Test %g")]
    public static void Test()
    {
        Debug.Log("Test");
    }
    #endif

    public static T GetRandom<T>(this T[] array)
    {
        if(array.Length == 0)
        return default(T);

        return array[Random.Range(0, array.Length)];
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static string PlayerTag()
    {
        return "Player";
    }
}
