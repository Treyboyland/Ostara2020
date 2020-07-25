using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            var chosenIndex = Random.Range(i, list.Count);

            var temp = list[i];
            list[i] = list[chosenIndex];
            list[chosenIndex] = temp;
        }
    }
}
