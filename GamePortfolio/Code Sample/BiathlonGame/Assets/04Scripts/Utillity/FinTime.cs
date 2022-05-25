using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FinTime
{
    public string athleteName;
    public float finTime;

    public FinTime(string name, float time)
    {
        athleteName = name;
        finTime = time;
    }
}
