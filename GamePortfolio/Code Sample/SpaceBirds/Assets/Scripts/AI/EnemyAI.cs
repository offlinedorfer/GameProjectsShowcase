using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth = 9;
    private int currentHealth;


    [SerializeField] float speed;
    [SerializeField] float timer = 0;

    float yDeviation;
    float minDeviation = 3.0f;
    float maxDeviation = 7.0f;

    bool isGoingUp;

    private void Start()
    {
        isGoingUp = true;
        currentHealth = maxHealth;
        yDeviation = Random.Range(minDeviation, maxDeviation);
    }

    void Update()
    {
        AIMovement();
        Destroy();
    }

    private void Destroy()
    {
        if (transform.position.x < -WorldBounds.instance.xBound)
        {
            FindObjectOfType<Player>().GetComponent<Player>().SubtracktHelath();
            AudioManager.instance.PlaySound(AudioManager.Sound.PlayerDie);
            Destroy(gameObject);
        }
    }

    private void AIMovement()
    {
        float yTranslation = Mathf.Lerp(-yDeviation, yDeviation, timer);

        if (timer >= 1.0f) { isGoingUp = false; }
        if (timer <= 0.0f) { isGoingUp = true; }
        if (isGoingUp) timer += Time.deltaTime;
        else timer -= Time.deltaTime;

        Vector2 translation = new Vector2(-Time.deltaTime * speed, yTranslation * Time.deltaTime);
        transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO Change into events
        if(collision.tag == "Bullet")
        {
            AudioManager.instance.PlaySound(AudioManager.Sound.EnemyHit);
            TakeDamage(4);
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            GameManager.instance.AddScore(10);
            AudioManager.instance.PlaySound(AudioManager.Sound.EnemyDie);
            Destroy(gameObject);
        }
}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
