using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] PowerUp powerUp;

    float minSpawnTime = 5.0f;
    float maxSpawnTime = 10.0f;

    float xOffset = 2.0f;

    private void Start() => TriggerRespawn();

    public void TriggerRespawn()
    {
        float randomWaitTime = Random.Range(minSpawnTime, maxSpawnTime);
        StartCoroutine(RespawnTimer(randomWaitTime));
    }
    IEnumerator RespawnTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        RespawnPowerUp();
    }
    void RespawnPowerUp()
    {
        Vector2 randomPosition = new Vector2(WorldBounds.instance.xBound + xOffset, Random.Range(-WorldBounds.instance.yBound, WorldBounds.instance.yBound));
        Instantiate(powerUp, randomPosition, Quaternion.identity);
    }


}
