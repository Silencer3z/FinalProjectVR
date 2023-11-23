using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnOnGround : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemy()
    {
        
        RaycastHit hit;
        Vector3 spawnPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {

                spawnPosition = hit.point;
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }


        }
    } 
    
}
