using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPositions : Photon.MonoBehaviour
{
    public Vector3 player1SpawnPos;
    public Quaternion player1SpawnRot = new Quaternion(0, 0, 0, 0);
    public Vector3 player2SpawnPos;
    public Quaternion player2SpawnRot;

    // Start is called before the first frame update
    void Start()
    {
        player1SpawnPos = new Vector3(0, 0.5f, -1.4f);
        player1SpawnRot = Quaternion.Euler(-13, 0, 0);
        player2SpawnPos = new Vector3(0, 0.5f, 1.4f);
        player2SpawnRot = Quaternion.Euler(-13, 180, 0);
    }
}
