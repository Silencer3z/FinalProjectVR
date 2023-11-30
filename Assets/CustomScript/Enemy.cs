using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 10;
    public int enemyHP = 100;
    public int damageAmount;
    NavMeshAgent agent;
    GameObject target;

    public AudioClip hitMarker;
    private AudioSource hitMarkerSource;
    public AudioClip enemyDead;
    private AudioSource enemyDeadSource;

    
    // Start is called before the first frame update
    void Start()
    {
        hitMarkerSource = gameObject.AddComponent<AudioSource>();
        hitMarkerSource.clip = hitMarker;
        hitMarkerSource.playOnAwake = false;

        enemyDeadSource = gameObject.AddComponent<AudioSource>();
        enemyDeadSource.clip = enemyDead;
        enemyDeadSource.playOnAwake = false;

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
            playhitMarker();
            Debug.Log("hits");
            enemyHP--;
            Destroy(other.gameObject);
            
        }
    }

    void PlayEnemyDead()
    {
        if(enemyDeadSource != null && enemyDead != null)
        {

            enemyDeadSource.PlayOneShot(hitMarker);
        }
    }
    void playhitMarker()
    {
        if (hitMarkerSource != null && hitMarker != null)
        {
         
            hitMarkerSource.PlayOneShot(hitMarker);
        }
    }


}