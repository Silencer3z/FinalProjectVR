using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHP = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHP == 0)
        {
            Death();
        }

    }
    public void Death()
    {
        Time.timeScale = 0;
        Debug.Log("you dead");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerHP--;
        }
    }
}
