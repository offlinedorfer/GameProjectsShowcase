using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedoMeter : MonoBehaviour
{
    public TextMeshProUGUI speed;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        PrintCurrentSpeed();
    }
    void PrintCurrentSpeed()
    {
        int currentSpeedinKMH = Mathf.RoundToInt(_playerMovement.resSpeed * 3.6f);
        speed.text = "Speed: " + currentSpeedinKMH;
    }
}
