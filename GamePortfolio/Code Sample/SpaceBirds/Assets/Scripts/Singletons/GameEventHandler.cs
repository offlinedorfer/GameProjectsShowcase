using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventHandler : MonoBehaviour
{
    public static GameEventHandler instance;
    public event Action onHealthPack;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public void HealthPackEvent() 
    {
        if (onHealthPack != null)
        { 
            onHealthPack(); 
        } 
    }
}
