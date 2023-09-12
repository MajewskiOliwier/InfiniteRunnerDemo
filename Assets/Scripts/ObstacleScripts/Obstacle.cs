using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    private enum Line{
        Left = -12,
        BetweeenLeft = -6,
        Middle = 0,
        BetweeenRight = 6,
        Right = 12
    }

    protected float currentLine = (int)Line.Middle;

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision occured");
        if(collision.transform.tag == "Player"){
            checkHitType(collision);
        }
    }

    protected abstract void checkHitType(Collision collision);

    
}
