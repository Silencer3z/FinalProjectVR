using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float spawnInterval = 1f;
    public float spawnRadius = 20f;
    public float spawnYPosition = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                 {
                Vector3 spawnPoint = hit.point; // Get the ground position
                spawnPoint.y = spawnYPosition; // Set the Y position

                Instantiate(EnemyPrefab, spawnPoint, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }
            Ray ray = Camera.main.ScreenPointToRay(transform.position);

            
        
         
        }
    }

}
