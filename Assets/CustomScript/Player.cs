using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHP = 1;
    public TextMeshProUGUI hpText;
    public GameObject DeadText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "HP : "+ playerHP.ToString();
        if(playerHP == 0)
        {
            Death();
        }

    }
    public void Death()
    {
        Time.timeScale = 0;
        Debug.Log("you dead");
        DeadText.SetActive(true);
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerHP--;
            Destroy(other.gameObject);
        }
    }
}
