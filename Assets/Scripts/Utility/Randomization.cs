using UnityEngine;
using System.Collections;

public static class Randomization
{
    public static T RandomObject<T>(T[] objects)
    {
        return objects[Random.Range(0, objects.Length)];
    }
}
