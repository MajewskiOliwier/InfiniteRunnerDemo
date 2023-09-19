using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextArea; 
    [SerializeField] private float scoreDistanceMultiplier = 1.0f;  //update it
    
    private int totalScore = 1;
    private int coinScore = 0;

    [SerializeField] private bool isCoinScoreBoostActive = false;
    [SerializeField] private float coinBoostDuration = 10f;
    private Coroutine coinBoostCoroutine;

    

    public void UpdateTotalScore(int distanceScore){
        totalScore = (int)Math.Floor(distanceScore * scoreDistanceMultiplier) + coinScore;
        scoreTextArea.text = "Score : "+ totalScore;
        
    }

    private void Update(){
        Debug.Log("isCoinScoreBoostActive = " + isCoinScoreBoostActive);
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

    IEnumerator ActivateBoost(){
        Debug.Log("coroutineActive");
        isCoinScoreBoostActive = true;
        
        yield return new WaitForSeconds(coinBoostDuration);

        Debug.Log("coroutine no more Active");
        isCoinScoreBoostActive = false;
    }
}
