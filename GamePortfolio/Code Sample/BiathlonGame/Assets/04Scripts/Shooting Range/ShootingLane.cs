using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLane : MonoBehaviour
{
    public GameObject[] targets;
    private int _penaltyCount;

    public int GetPenaltyCount()
    {
        foreach (GameObject target in targets)
        {
            if (target.GetComponent<Renderer>().material.color == Color.white)
            {
                target.GetComponent<Renderer>().material.color = Color.black;
            }
            else { _penaltyCount++; }
        }
        return _penaltyCount;
    }

}
