using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class UsefulStuff 
{
    private static System.Random rng = new(); // used for shuffling lists

    public static void ShuffleList<T>(List<T> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }
    public static int GetRandomIndex<T>(List<T> list)
    {
        return rng.Next(0, list.Count);
    }
}
