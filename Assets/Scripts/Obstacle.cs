using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event EventHandler OnColliedEndGame;
    
    private void OnCollisionEnter(Collision collision) {
        
        if(collision.transform.tag == "Player"){
            Debug.Log("Collide with player");
            OnColliedEndGame?.Invoke(this, EventArgs.Empty);
            collision.gameObject.GetComponent<MainCharacterAnimationSelector>().CheckHitLocationX(this.gameObject.transform.GetChild(0).gameObject.transform);
            
        }
    }


}
