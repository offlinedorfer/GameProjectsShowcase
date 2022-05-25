using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 10;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    virtual protected void Move()
    {
        Vector2 moveDirection = new Vector3(speed * Time.deltaTime, 0, 0);
        transform.Translate(moveDirection);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
