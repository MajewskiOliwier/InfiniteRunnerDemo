using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    private Animator mainCharacterAnimator;
    private bool isDead = false;

    [Tooltip("Variables for calculating distance")]
    private float oldPositionY;
    private float totalDistanceTraveledY = 0;

    //private bool doubleCoinScore ?? 
    
    private void Awake(){
        mainCharacterAnimator = gameObject.GetComponent<Animator>();
        Obstacle.OnFinalCollision += Obstacle_MainCharacterDeath;
    }

    private void Start(){
        oldPositionY = transform.position.z;
    }

    private void Update(){
        if(!isDead){
            float distanceThisFrameY = transform.position.z - oldPositionY;
            totalDistanceTraveledY += distanceThisFrameY;
            oldPositionY = transform.position.z;
            scoreManager.UpdateTotalScore((int)Math.Floor(totalDistanceTraveledY));
        }   
    }

    private void Obstacle_MainCharacterDeath(object sender, EventArgs e){
        Debug.Log("Player is dead !!!!");
        isDead = true;
    }

    public Animator getMainCharacterAnimator(){
        return mainCharacterAnimator;
    }
    
    public bool GetIsDead(){
        return isDead;
    }
}
