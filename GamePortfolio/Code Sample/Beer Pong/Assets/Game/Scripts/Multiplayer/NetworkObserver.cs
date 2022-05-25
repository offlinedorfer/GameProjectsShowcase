using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObserver : Photon.MonoBehaviour
{
    public GameObject myCamera;

    // Start is called before the first frame update
    void Start()
    {
        if(photonView.isMine)
        {
            myCamera.SetActive(true);
            GetComponent<ExtendedFlycam>().enabled = true;
            GetComponent<Scoreboard>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
