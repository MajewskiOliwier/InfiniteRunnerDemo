using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    //animation variables
    [SerializeField] private Vector3 rotation_vector; 
    [SerializeField] private float rotation_speed;

    //pick up effect variables
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int coinScoreValue = 5;


    private void Update() {
        ItemAnimation();
    }
   
    protected override void PickUpItem(){
        scoreManager.AddCoinScore(coinScoreValue);
    }
   
    protected override void ItemAnimation(){
        transform.Rotate(rotation_vector * rotation_speed * Time.deltaTime);
    }
}
