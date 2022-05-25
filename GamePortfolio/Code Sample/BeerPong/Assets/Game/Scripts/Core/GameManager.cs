using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int redCupCount = 10;
    public int blueCupCount = 10;
    public TextMeshProUGUI redCount;
    public TextMeshProUGUI blueCount;
    public TextMeshProUGUI gameOver;

    public GameObject gameUI;
    public GameObject pauseMenu;
    public bool isRedTeam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CupCount();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnablePauseMenu();
        }
        
    }

    void CupCount()
    {
        redCount.text = "Team Rot Becher Übrig: " + redCupCount;
        blueCount.text = "Team Blau Becher Übrig: " + blueCupCount;

        if (redCupCount <= 0)
        {
            gameOver.gameObject.SetActive(true);
            gameOver.text = "Team Blau Gewinnt";
            gameOver.color = Color.blue;
        }
        else if (blueCupCount <= 0)
        {
            gameOver.gameObject.SetActive(true);
            gameOver.text = "Team Rot Gewinnt";
            gameOver.color = Color.red;
        }
        else
        {
            gameOver.gameObject.SetActive(false);
        }
    }



    #region Button Functions

    void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    #endregion

}
