using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    Transform player;
    Vector2 selfToPlayer;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        selfToPlayer = (player.position - transform.position).normalized;
    }

    override protected void Move()
    {
        transform.Translate(selfToPlayer * speed * Time.deltaTime);
    }


}
