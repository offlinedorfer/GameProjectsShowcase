using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : Photon.MonoBehaviour
{
    //public GameManager gameManager;
    private bool alreadyTriggered;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        PhotonView playerView = other.GetComponent<PhotonView>();
        if (!alreadyTriggered && playerView.isMine)
        {
            alreadyTriggered = true;
            StartCoroutine(WaitForDestroy());
        }
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(2);
        this.photonView.RPC("DestroyCup", PhotonTargets.All);
    }

    [PunRPC]
    private void DestroyCup()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
