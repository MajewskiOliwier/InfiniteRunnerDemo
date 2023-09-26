using System;
using System.Collections;
using UnityEngine;

[System.Serializable] 
public class Leaderboard
{
    private int[] TopScores = {0,0,0,0,0,0,0,0,0,0};
    

    //lunch after player death
    public void UpdateScoreBoard(int newRecord){
        int lowestScoreValue = 0;
        for(int i = 9; i >= 0; i--){ //this loop prevents values from multiplying
            if(TopScores[i] > 0){
                lowestScoreValue = TopScores[i];
            }
        }

        if(newRecord > lowestScoreValue){
            TopScores[9] = newRecord;
            Quicksort(0,9); 
            SerializationManager.Save("leaderboardScore",TopScores);
        }
    }

    public void Test(){  
        Debug.Log("LEADERBOARD = "+ TopScores[0]+ ", "+TopScores[1]+ ", "+TopScores[2]+ ", "+TopScores[3]+ ", "+TopScores[4]+ ", "+TopScores[5]+ ", "+TopScores[6]+ ", "+TopScores[7]+ ", "+TopScores[8]+ ", "+TopScores[9]);
    }

    public void SetTopScores(){
        
        var topScoresArray = (int[])SerializationManager.Load("leaderboardScore");
        
        
        if(topScoresArray != null){
        
            Debug.Log("LEADERBOARD Loaded = "+ topScoresArray[0]+ ", "+topScoresArray[1]+ ", "+topScoresArray[2]+ ", "+topScoresArray[3]+ ", "+topScoresArray[4]+ ", "+topScoresArray[5]+ ", "+topScoresArray[6]+ ", "+topScoresArray[7]+ ", "+topScoresArray[8]+ ", "+topScoresArray[9]);
    
            TopScores = topScoresArray;
        }else{
            Debug.Log("loadTopScoresDoesnt work1");
        }
    }

    private void Quicksort(int left, int right){
        int middle = (int)(left+right)/2, pivot, border, i;

        pivot = TopScores[middle];
        TopScores[middle] = TopScores[right];

        border = left;
        i = left;

        while(i < right){
            if(TopScores[i] > pivot){
                (TopScores[border], TopScores[i]) = (TopScores[i], TopScores[border]);
                border++;
            }
            i++;
        }

        TopScores[right] = TopScores[border];
        TopScores[border] = pivot;
        if(left < border-1){
            Quicksort(left,border-1);
        }
        if(border+1 < right){
            Quicksort(border+1,right);
        }
    }
}
