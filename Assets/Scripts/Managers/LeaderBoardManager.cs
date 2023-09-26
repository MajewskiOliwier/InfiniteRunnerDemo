using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    private static Leaderboard leaderboardInstance;

    private void Awake(){
        DontDestroyOnLoad(gameObject);
    }

    public static Leaderboard GetLeaderboard(){
        if (leaderboardInstance == null){
            leaderboardInstance = new Leaderboard();
        }
        return leaderboardInstance;
    }
}
