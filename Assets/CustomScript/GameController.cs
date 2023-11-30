using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public float timeInSeconds = 60f; // Set the time limit in seconds
    public TextMeshProUGUI timeText;// Reference to the UI text to display time
    public GameObject fixUI;
    public GameObject winUI;
    private float currentTime;

    void Start()
    {
        Time.timeScale = 1f;

        currentTime = timeInSeconds;
    }

    void Update()
    {
        
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            DisplayTime(currentTime);
        }
        else
        {
            // Time's up, do something here like end the game or take appropriate action
            Debug.Log("Time's up!");
            fixUI.SetActive(false);
            winUI.SetActive(true);
            Time.timeScale = 0f;

        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Convert time to minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the UI text to display remaining time
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
  
   
}

