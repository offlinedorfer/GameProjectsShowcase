using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour
{
    private AudioSource waterSound;
    private PhotonView photonView;

    private void Start()
    {
        waterSound = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PhotonView playerView = other.GetComponent<PhotonView>();
        if(playerView.isMine)
        {
            this.photonView.RPC("PlayWaterSound", PhotonTargets.All);
        }

    }

    [PunRPC]
    void PlayWaterSound()
    {
        waterSound.Play();
    }
}
