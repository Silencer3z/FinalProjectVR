using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 10;
    public int enemyHP = 100;
    NavMeshAgent agent;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        Seek(target.transform.position);
        if (enemyHP == 0) 
        {
            Destroy(this.gameObject);

            ScoreManager.Instance.AddScore(scoreValue);
        }
        
 
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) 
        {
            
            Debug.Log("hits");
            enemyHP--;
            Destroy(other.gameObject);
            
        }
        if (other.CompareTag("Player")) 
        {
            Destroy(this.gameObject);
        }
    }


}