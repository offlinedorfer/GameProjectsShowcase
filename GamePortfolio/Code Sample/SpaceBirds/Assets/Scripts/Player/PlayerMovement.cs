using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 20)]
    [SerializeField] float speed = 10;

    float xBound;
    float yBound;

    private void Start()
    {
        xBound = WorldBounds.instance.xBound;
        yBound = WorldBounds.instance.yBound;
    }

    void Update()
    {
        CheckBounds();
        Movement();
    }
    void CheckBounds()
    {
        Vector2 currentPosition = transform.position;
        if (transform.position.x > xBound) { transform.position = new Vector2(xBound, transform.position.y); }
        if (transform.position.x < -xBound) { transform.position = new Vector2(-xBound, transform.position.y); }
        if (transform.position.y > yBound) { transform.position = new Vector2(transform.position.x, yBound); }
        if (transform.position.y < -yBound) { transform.position = new Vector2(transform.position.x, -yBound); }
    }

    void Movement()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 translation = new Vector3(horizontal, vertical);

        transform.Translate(translation * Time.deltaTime * speed);
    }

}