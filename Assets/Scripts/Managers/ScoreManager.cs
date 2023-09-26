using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }    //no need for multiple ScoreManagers
    [SerializeField] private TextMeshProUGUI scoreTextArea; 
    [SerializeField] private float scoreDistanceMultiplier = 1.0f;  //update it
    
    
    [SerializeField] private int totalScore;
    private int coinScore = 0;

    [SerializeField] private bool isCoinScoreBoostActive = false;
    [SerializeField] private float coinBoostDuration = 10f;
    private Coroutine coinBoostCoroutine;

    [SerializeField] LeaderBoardManager leaderboardManager;
    Leaderboard leaderboard;

    private void Awake(){
        if(Instance != null){
            Debug.LogError("There is more than one ScoreManager"+ transform + " - "+ Instance);
            Destroy(gameObject);
            return;
        }
            
        Instance = this;
        Obstacle.OnFinalCollision += Obstacle_MainCharacterDeath;
        leaderboard = LeaderBoardManager.GetLeaderboard();
    }


    private void Obstacle_MainCharacterDeath(object sender, EventArgs e){
        Debug.Log("current total score =  " + totalScore);
        leaderboard.UpdateScoreBoard(getTotalScore());
        leaderboard.Test();
    }

    public void UpdateTotalScore(int distanceScore){
        totalScore = (int)Math.Floor(distanceScore * scoreDistanceMultiplier) + coinScore;
        scoreTextArea.text = "Score : "+ totalScore;
        
    }

    private void Start(){
        leaderboard.Test();
    }

    public void AddCoinScore(int additionalCoinScore){
        if(isCoinScoreBoostActive){
            additionalCoinScore *= 2;
        }
        coinScore += additionalCoinScore;
    }

    public void SetCoinBoostActive(){

        if(coinBoostCoroutine == null){
            coinBoostCoroutine = StartCoroutine(ActivateBoost());
        }else{
            StopCoroutine(coinBoostCoroutine);
            isCoinScoreBoostActive = false;
            coinBoostCoroutine = StartCoroutine(ActivateBoost());
        }
    }

    public int getTotalScore(){
        return totalScore;
    }

    IEnumerator ActivateBoost(){
        isCoinScoreBoostActive = true;
        
        yield return new WaitForSeconds(coinBoostDuration);

        isCoinScoreBoostActive = false;
    }
}
