using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public float spawnTime = 3f;
    public float playerProximityThreshold = 10f; // Adjust this threshold as needed
    public Transform[] spawnPoints;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player tag is "Player"
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void SpawnEnemy()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        if (Vector3.Distance(player.position, spawnPoint.position) > playerProximityThreshold)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Check the type of spawned enemy to determine additional spawns
            if (enemyPrefab.CompareTag("Enemy1"))
            {
                // Spawn two additional Enemy2
                for (int i = 0; i < 2; i++)
                {
                    SpawnChildEnemy("Enemy2", spawnPoint);
                }
            }
            else if (enemyPrefab.CompareTag("Enemy2"))
            {
                // Spawn one additional Enemy3
                SpawnChildEnemy("Enemy3", spawnPoint);
            }
            // Add more conditions for other types of enemies if needed
        }
    }

    // Spawn a specific type of enemy at the given spawn point
    void SpawnChildEnemy(string enemyTag, Transform spawnPoint)
    {
        GameObject childEnemy = GetRandomEnemyPrefabOfType(enemyTag);
        Instantiate(childEnemy, spawnPoint.position, spawnPoint.rotation);
    }

    // Get a random enemy prefab from the array
    GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    // Get a random enemy prefab of a specific type
    GameObject GetRandomEnemyPrefabOfType(string enemyType)
    {
        GameObject[] availableEnemies = GetAvailableEnemies(enemyType);
        int randomIndex = Random.Range(0, availableEnemies.Length);
        return availableEnemies[randomIndex];
    }

    // Filter available enemy prefabs by type
    GameObject[] GetAvailableEnemies(string enemyType)
    {
        return System.Array.FindAll(enemyPrefabs, enemy => enemy.CompareTag(enemyType));
    }
}
