using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{

    public AudioSource soundEffect; // Reference to AudioSource component for sound effects
    public void MainMenu()
    {
        PlaySceneTransitionSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        PlaySceneTransitionSound();
        SceneManager.LoadScene("SampleScene");
    }
    public void StartGame()
    {
        StartCoroutine(LoadSceneAfterDelay("SampleScene"));
    }

    public void ExitD()
    {
        StartCoroutine(QuitGameAfterDelay());
    }
   

    

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        PlaySceneTransitionSound();
        yield return new WaitForSeconds(1.0f); // Wait for 1 second

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator QuitGameAfterDelay()
    {
        PlaySceneTransitionSound();
        yield return new WaitForSeconds(1.0f); // Wait for 1 second

        Application.Quit();
    }

    private void PlaySceneTransitionSound()
    {
        if (soundEffect != null && soundEffect.clip != null)
        {
            soundEffect.PlayOneShot(soundEffect.clip); // Play the scene transition sound
        }
        // You might want to add a delay here based on the audio clip length before loading the scene
    }
}
