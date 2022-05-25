using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class NetworkManager : Photon.MonoBehaviour
{
    public Text roomListing;
    public Text observerPW;
    public GameObject mainMenuUI;
    public GameObject lobbyUI;
    public GameObject menuCam;
    public Transform p1Spawn;
    public Transform p2Spawn;
    public Transform p3Spawn;
    public Transform p4Spawn;
    public Transform p5Spawn;
    public Transform observerSpawn;

    private bool isObserver;

    private void Awake()
    {
        mainMenuUI.SetActive(false);
        menuCam.GetComponent<Camera>().farClipPlane = 10;
        StartCoroutine(ActivateMainMenu());
        Application.targetFrameRate = 100;
    }

    IEnumerator ActivateMainMenu()
    {
        Quaternion camRot = Quaternion.Euler(180, 0, 180);
        yield return new WaitForSeconds(3);
        menuCam.SetActive(true);
        menuCam.transform.rotation = camRot;
        menuCam.GetComponent<Camera>().farClipPlane = 1000;
        GetComponentInChildren<VideoPlayer>().enabled = false;
        mainMenuUI.SetActive(true);
    }

    #region Server Connecting
    public void Connect()
    {
        // Verbindung zum Photon Network

        PhotonNetwork.ConnectUsingSettings("v02");
    }
    void OnConnectedToMaster()
    {
        Debug.Log("connected to master... loading lobby");
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        Debug.Log("Connected to lobby");
        mainMenuUI.SetActive(false);
        lobbyUI.SetActive(true);
        CheckIfObserver();
        //PhotonNetwork.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        string roomName = "Raum #";
        int numberOfRooms = 0;
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            numberOfRooms++;
        }
        if(numberOfRooms < 5)
            PhotonNetwork.CreateRoom(roomName + UnityEngine.Random.Range(0, 20));
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(GameObject.Find("Room To Join Text").GetComponent<Text>().text);
    }

    private void CheckIfObserver()
    {
        if(observerPW.text == "lol")
        {
            isObserver = true;
        }
    }

    void OnJoinedRoom()
    {
        if(isObserver)
        {
            SpawnAsObserver();
        }
        else if(!isObserver)
        {
            SpawnAsPlayer();
        }
        mainMenuUI.SetActive(false);
        menuCam.SetActive(false);
        lobbyUI.SetActive(false);
    }

    void SpawnAsObserver()
    {
        PhotonNetwork.Instantiate("Observer", observerSpawn.position, observerSpawn.rotation, 0);
    }

    void SpawnAsPlayer()
    {
        Vector3 playerSpawnPoint;
        Quaternion playerSpawnRot;
        if (PhotonNetwork.otherPlayers.Length == 0)
        {
            playerSpawnPoint = p1Spawn.position;
            playerSpawnRot = p1Spawn.rotation;
            PhotonNetwork.Instantiate("Player 1", playerSpawnPoint, playerSpawnRot, 0);
        }
        //else if(PhotonNetwork.otherPlayers.Length == 1)
        //{
        //    playerSpawnPoint = p2Spawn.position;
        //    playerSpawnRot = p2Spawn.rotation;
        //    PhotonNetwork.Instantiate("Player 3", playerSpawnPoint, playerSpawnRot, 0);
        //}
        else if(PhotonNetwork.otherPlayers.Length == 1)
        {
            playerSpawnPoint = p3Spawn.position;
            playerSpawnRot = p3Spawn.rotation;
            PhotonNetwork.Instantiate("Player 3", playerSpawnPoint, playerSpawnRot, 0);
        }
        //else if (PhotonNetwork.otherPlayers.Length == 3)
        //{
        //    playerSpawnPoint = p4Spawn.position;
        //    playerSpawnRot = p4Spawn.rotation;
        //    PhotonNetwork.Instantiate("Player 4", playerSpawnPoint, playerSpawnRot, 0);
        //}
        //else if (PhotonNetwork.otherPlayers.Length == 4)
        //{
        //    playerSpawnPoint = p5Spawn.position;
        //    playerSpawnRot = p5Spawn.rotation;
        //    PhotonNetwork.Instantiate("Player 5", playerSpawnPoint, playerSpawnRot, 0);
        //}
    }


    // Update is called once per frame
    void Update()
    {
        roomListing.text = "Verfügbare Räume: ";
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            roomListing.text += "\n" + room.Name + ", verbundene Spieler: " + room.PlayerCount;
        }

        //Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
        //Debug.Log("Anzahl weiterer Spieler: " + PhotonNetwork.otherPlayers.Length);
    }

    public void SaveNickName()
    {
        string nickName = GameObject.Find("Nickname Text").GetComponent<Text>().text.ToString();
        // Nickname finden und zwischenspeichern
        PhotonNetwork.player.NickName = nickName;
    }

    public void ActivateSnow()
    {
        GameObject snow = GameObject.Find("Snowflakes").transform.Find("Snow").gameObject;
        if (!snow.activeInHierarchy)
            snow.SetActive(true);

        else if(snow.activeInHierarchy)
            snow.SetActive(false);
    }

    #endregion
    public void QuitGame()
    {
        Application.Quit();
    }
}
