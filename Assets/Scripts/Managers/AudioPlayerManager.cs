using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerManager : MonoBehaviour
{
    private void Awake(){
        Obstacle.OnFinalCollision += Obstacle_MainCharacterDeath;
    }

    private void Obstacle_MainCharacterDeath(object sender, EventArgs e){
        Debug.Log("lunch death SFX");
    }
}
