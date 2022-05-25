using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayer : Photon.PunBehaviour, IPunObservable
{
    public GameObject myCamera;
    public GameObject myCanvas;
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        if (photonView.isMine)
        {
            myCamera.SetActive(true);
            if(myCanvas != null)
                myCanvas.SetActive(true);
            GetComponent<Scoreboard>().enabled = true;

            if(GetComponent<BallController>() != null)
                GetComponent<BallController>().enabled = true;
        }
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 15);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 15);
        }
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // we own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
