using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterManager : MonoBehaviour
{
    private Animator mainCharacterAnimator;
    
    private void Awake(){
        mainCharacterAnimator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        Obstacle.OnColliedEndGame += Obstacle_OnColliedEndGame;
    }

    private void Obstacle_OnColliedEndGame(object sender, EventArgs e){
        Debug.Log("Finish this round");
    }

    public Animator getMainCharacterAnimator(){
        return mainCharacterAnimator;
    }
}
