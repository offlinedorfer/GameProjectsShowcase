using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPresenter : MonoBehaviour
{
    public static UIPresenter instance;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI restartHintText;

    bool quitButtonPressed = false;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    public void SetLivesText(int lives) => livesText.text = "lives: " + lives;
    public void SetScoreText(int score) => scoreText.text = "score: " + score;
    public void SetGameOverText() => gameOverText.gameObject.SetActive(true);
    public void SetRestartHintText() => restartHintText.gameObject.SetActive(true);
}
