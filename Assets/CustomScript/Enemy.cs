using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
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
            enemyHP = -1;
            Destroy(other.gameObject);
        }
    }


}