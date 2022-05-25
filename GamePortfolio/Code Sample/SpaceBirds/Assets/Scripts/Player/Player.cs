using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int liveCount = 3;

    private void Start()
    {
        UIPresenter.instance.SetLivesText(liveCount);
        GameEventHandler.instance.onHealthPack += AddHealth;
    }
    private void OnDisable() {GameEventHandler.instance.onHealthPack += AddHealth;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnemyBullet" || collision.transform.tag == "Enemy")
        {
            AudioManager.instance.PlaySound(AudioManager.Sound.PlayerDie);
            Destroy(collision.gameObject);
            SubtracktHelath();
        }
        else if(collision.transform.tag == "HealthPack")
        {
            GameEventHandler.instance.HealthPackEvent();
            AudioManager.instance.PlaySound(AudioManager.Sound.PowerUp);
            Destroy(collision.gameObject);
        }
    }

    void AddHealth()
    {
        liveCount++;
        if(liveCount > 5)
        {
            liveCount = 5;
        }
        UIPresenter.instance.SetLivesText(liveCount);
    }
    public void SubtracktHelath()
    {
        liveCount--;
        if (liveCount <= 0)
        {
            GameManager.instance.SetGameOver();
            AudioManager.instance.PlaySound(AudioManager.Sound.PlayerDie);
            Destroy(gameObject);
            Time.timeScale = 0;

        }

        UIPresenter.instance.SetLivesText(liveCount);
    }
}
