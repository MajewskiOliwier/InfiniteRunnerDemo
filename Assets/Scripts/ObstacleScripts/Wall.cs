using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{
    
    //Lamp size x = 10  , y = 10, z = 3  Center x = 0 , y = 5, z = 0
    protected override void checkHitType(Collision collision){
        Vector3 contactPoint = collision.contacts[0].point;
        GameObject player = collision.gameObject;

        float contactPointX = (collision.contacts[0].point.x + collision.contacts[1].point.x)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        float contactPointY = (collision.contacts[0].point.y + collision.contacts[1].point.y)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        float contactPointZ = (collision.contacts[0].point.z + collision.contacts[1].point.z)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        
        float wallWidth = 10f/2f;
        bool wasWarningIssued  = player.GetComponent<MainCharacterAnimationSelector>().GetWarningIssued();

        int currentLineNumber = player.GetComponent<MainCharacterChangeLanes>().GetCurrentLineXPosition();
        int preChangeLineNumber = player.GetComponent<MainCharacterChangeLanes>().GetPreChangePosition();

        Debug.Log(this.gameObject.name + " : collision in x dimension : " + contactPointX + " ,collision in y dimension : " + contactPointY + " Local x pos = " + GetLocalHitLocationInXPos(wallWidth,contactPointX));
        //Debug.Log("currentLineNumber = " + currentLineNumber + " preChangeLineNumber = " + preChangeLineNumber);
        
        Vector3 localCollisionPoint = transform.InverseTransformPoint(contactPoint);
        Debug.Log(this.gameObject.name + "local hit location with my function:  + " + GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ));
        


        //add splash mid wall ??

        Debug.Log("Stats:  currentline = " +currentLineNumber + " prechangeNumber"+  preChangeLineNumber + " local hit z pos" + GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ));
            
        if(GetLocalHitLocationInXPos(wallWidth,contactPointX) > 8f){ //occurs when bumping to the side 
            if(wasWarningIssued || ((currentLineNumber < preChangeLineNumber) && GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ) >= 1.49)){ //if the (int) currentLine is lower than preChangeLine then it means that the collision occured after changing lane to the left into the ,,warning zone" 
                OnFinalCollisionEvent(EventArgs.Empty);
                Debug.Log("Death Animation to the right");
            }else{ 
                if(GetLocalHitLocationInXPos(wallWidth,contactPointX) >= 10f){
                    OnWarningBounceBackToPreChangeLaneEvent(EventArgs.Empty);
                }
                Debug.Log("Player bumped from the right side");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
                
            }
        }else if(GetLocalHitLocationInXPos(wallWidth,contactPointX) < 2f){

            if(wasWarningIssued || ((currentLineNumber > preChangeLineNumber) && GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ) >= 1.49)){
                OnFinalCollisionEvent(EventArgs.Empty);
                Debug.Log("Death Animation to the left");
            }else{ 
                if(GetLocalHitLocationInXPos(wallWidth,contactPointX) <= 0f){
                    OnWarningBounceBackToPreChangeLaneEvent(EventArgs.Empty);
                }
                Debug.Log("Player bumped from the left side");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }else{
            OnFinalCollisionEvent(EventArgs.Empty);
            Debug.Log("Run straight into wall");
        }
    }

}
