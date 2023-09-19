using System.Collections;
using UnityEngine;

[System.Serializable]
public class Leaderboard
{
    private int[] TopScores = {0,0,0,0,0,0,0,0,0,0};
    
    private void Start() {
        
        //Debug.Log(TopScores[0]+TopScores[1]+TopScores[2]+TopScores[3]+TopScores[4]+TopScores[5]+TopScores[6]+TopScores[7]+TopScores[8]+TopScores[9]);
        
    }

    //lunch after player death
    public void UpdateScoreBoard(int newRecord){
        if(newRecord > TopScores[9]){
            TopScores[9] = newRecord;
            //save
        }
    }

    private void Quicksort(int left, int right){
        int middle = (int)(left+right)/2, pivot, border, i;

        pivot = TopScores[middle];
        TopScores[middle] = TopScores[right];

        border = left;
        i = left;

        while(i < right){
            if(TopScores[i] < pivot){
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
