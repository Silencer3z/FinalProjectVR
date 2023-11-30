using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHP = 1;
    public TextMeshProUGUI hpText;
    public GameObject DeadText;

    public AudioClip deadSound;
    private AudioSource deadSoundSource; // Change to private to assign in code

    void Start()
    {
        // Get the AudioSource component from the current GameObject or add one if it doesn't exist
        deadSoundSource = gameObject.AddComponent<AudioSource>();
        // Assign the deadSound to the AudioSource
        deadSoundSource.clip = deadSound;
        // Ensure it doesn't play on awake
        deadSoundSource.playOnAwake = false;
    }

    void Update()
    {
        hpText.text = "HP: " + playerHP.ToString();
    }

    public void TakeDamage(int damage)
    {
        playerHP -= damage;

        if (playerHP <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Time.timeScale = 0;
        Debug.Log("You're dead");

        // Check if the AudioSource and deadSound are assigned
        if (deadSoundSource != null && deadSound != null)
        {
            // Play the dead sound
            deadSoundSource.PlayOneShot(deadSound);
        }

        DeadText.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                int damageAmount = enemy.damageAmount;
                TakeDamage(damageAmount);
            }

            Destroy(other.gameObject);
        }
    }
}
