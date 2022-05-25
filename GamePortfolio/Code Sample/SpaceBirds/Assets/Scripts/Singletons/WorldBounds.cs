using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    public static WorldBounds instance;
    public float xBound { get; private set; }
    public float yBound { get; private set; }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
        xBound = 8.6f;
        yBound = 4.75f;
    }



}
