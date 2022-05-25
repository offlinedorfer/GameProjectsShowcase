using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    Transform player;
    float XOfsset = 4.0f;

    private void Start()
    {
        InvokeRepeating("ConsiderShooting", Random.Range(0.5f, 2.0f), Random.Range(0.5f, 2.0f));
    }

    void ConsiderShooting()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        if (player.position.x < transform.position.x - XOfsset) Shoot();
    }

    void Shoot()
    {
        AudioManager.instance.PlaySound(AudioManager.Sound.EnemyShoot);
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
