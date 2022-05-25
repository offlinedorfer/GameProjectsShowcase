using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : Photon.MonoBehaviour
{
    GameObject pauseMenu;
    GameObject scoreboard;
    int playerCount;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreboard = GameObject.Find("Canvas").transform.Find("Scoreboard").gameObject;
        pauseMenu = GameObject.Find("Canvas").transform.Find("Pause Menu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }

        // Scoarboard
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // Show Scoreboard
            scoreboard.SetActive(true);
            UpdateScoreboard();
        }
        
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }



        if(PhotonNetwork.player.NickName == "")
        {
            PhotonNetwork.player.NickName = "Player #" + Random.Range(1, 100);
        }
    }

    void UpdateScoreboard()
    {
        // Check Playercount
        playerCount = PhotonNetwork.playerList.Length;
        // Get Player Names
        var playerNames = new StringBuilder();

        foreach (var player in PhotonNetwork.playerList)
        {
            print("Nickname: " + player.NickName);
            playerNames.Append(player.NickName + "\n");
        }

        // Scoreboard Text
        string output = "Anzahl Spieler: " + playerCount.ToString() + "\n" + playerNames.ToString();
        scoreboard.transform.Find("Text").GetComponent<Text>().text = output;
        scoreboard.transform.Find("Room Name").GetComponent<Text>().text = PhotonNetwork.room.Name;
    }
}
