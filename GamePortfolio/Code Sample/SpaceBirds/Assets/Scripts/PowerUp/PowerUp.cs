using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    [SerializeField] float speed = 5;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        if(FindObjectOfType<PowerUpSpawner>().gameObject.GetComponent<PowerUpSpawner>() != null)
        {
            FindObjectOfType<PowerUpSpawner>().gameObject.GetComponent<PowerUpSpawner>().TriggerRespawn();
        }
        Destroy(gameObject);
    }
}
