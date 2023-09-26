using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    private void OnEnable() {
        Obstacle.OnFinalCollision += Obstacle_MainCharacterDeath;
    }

    private void OnDisable() {
        Obstacle.OnFinalCollision -= Obstacle_MainCharacterDeath;
    }



    private void PauseGame(){
        Time.timeScale = 0f;
    }

    private void ResumeGame(){
        Time.timeScale = 1.0f;
    }

    private void Obstacle_MainCharacterDeath(object sender, EventArgs e){
        Debug.Log("prepare to reload level");
        
        RestartGame();
    }

    private void RestartGame(){
        StartCoroutine(WaitForRestart());
    }

    
    IEnumerator WaitForRestart(){

        yield return new WaitForSeconds(5f);
        Debug.Log("Wait for restart finished");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
