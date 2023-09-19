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

    static public event EventHandler OnWarningBounceBackToPreChangeLane;
    static public event EventHandler OnFinalCollision;

    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag == "Player"){
            checkHitType(collision);
        }
    }

    protected void OnWarningBounceBackToPreChangeLaneEvent(EventArgs e){
        var eh = OnWarningBounceBackToPreChangeLane;
        if(eh != null){
            eh(this, e);
        }
    }

    protected void OnFinalCollisionEvent(EventArgs e){
        var deathEvent = OnFinalCollision;
        if(deathEvent != null){
            deathEvent(this, e);
        }
    }

    protected abstract void checkHitType(Collision collision);

    public void setCurrentLine(int currentLineNumber){
        currentLine = currentLineNumber;
    }

    
    protected float GetLocalHitLocationInZPos(float depth, float contactPointZWorldPos){      //current point has to be inverted for the rest of the code to work
        return -1f * (contactPointZWorldPos - (contactPointZWorldPos- depth));
    }

    protected float GetLocalHitLocationInXPos(float width, float contactPointXWorldPos){
        return (width + (contactPointXWorldPos-currentLine));
    }
}
