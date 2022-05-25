using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] float spawnFrequency = 1;
    [SerializeField] float minSpawnDelay = 1.0f;
    [SerializeField] float maxSpawnDelay = 3.0f;

    float firstSpawnDelay = 1.0f;
    float xOffset = 2.0f;

    private void Start()
    {
        Invoke("Spawn", firstSpawnDelay);
    }

    void Spawn()
    {
        spawnFrequency = Random.Range(minSpawnDelay, maxSpawnDelay);
        Vector2 location = new Vector2(WorldBounds.instance.xBound + xOffset , Random.Range(-4.0f, 4.0f));
        Instantiate(enemyPrefab, location, Quaternion.identity);
        Invoke("Spawn", spawnFrequency);
    }
}
