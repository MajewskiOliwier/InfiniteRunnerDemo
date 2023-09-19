using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    
    

    private void PauseGame(){
        Time.timeScale = 0f;
    }

    private void ResumeGame(){
        Time.timeScale = 1.0f;
    }

    private void RestartGame(){
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
