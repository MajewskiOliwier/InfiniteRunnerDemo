using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event EventHandler OnColliedEndGame;
    
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collision works");
        OnColliedEndGame?.Invoke(this, EventArgs.Empty);
    }


}
