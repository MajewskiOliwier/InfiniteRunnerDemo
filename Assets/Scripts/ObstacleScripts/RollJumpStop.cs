using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollJumpStop : Obstacle
{
    //RollJumpStop size x = 10, y = 3.9 (uneven), z = 2;
    
    protected override void checkHitType(Collision collision){
        Vector3 contactPoint = collision.contacts[0].point;
        GameObject player = collision.gameObject;

        float contactPointX = (collision.contacts[0].point.x + collision.contacts[1].point.x)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        float contactPointY = (collision.contacts[0].point.y + collision.contacts[1].point.y)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        float contactPointZ = (collision.contacts[0].point.z + collision.contacts[1].point.z)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        
        Debug.Log(this.gameObject.name + " : collision in x dimension : " + contactPointX + " ,collision in y dimension : " + contactPointY);

        float roadblockWidth = 10f/2f;
        bool wasWarningIssued  = player.GetComponent<MainCharacterAnimationSelector>().GetWarningIssued();

        int currentLineNumber = player.GetComponent<MainCharacterChangeLanes>().GetCurrentLineXPosition();
        int preChangeLineNumber = player.GetComponent<MainCharacterChangeLanes>().GetPreChangePosition();
        
        Vector3 localCollisionPoint = transform.InverseTransformPoint(contactPoint);
        Debug.Log(this.gameObject.name + "local hit location with my function:  + " + GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ));
        

        if(contactPointY >= 3f){
            if(wasWarningIssued){
                Debug.Log("Death Animation by tipping over : " + contactPointY);
            }else{ 
                Debug.Log("Player tipped over but regain balance");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }else if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) > 8f){ //occurs when bumping to the side 
            
            if(wasWarningIssued || ((currentLineNumber < preChangeLineNumber) && GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ) >= 0.99)){ //if the (int) currentLine is lower than preChangeLine then it means that the collision occured after changing lane to the left into the ,,warning zone" 
                Debug.Log("Death Animation to the right");
            }else{ 
                if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) >= 10f){
                    
                    OnWarningBounceBackToPreChangeLaneEvent(EventArgs.Empty);
                }
                Debug.Log("Player bumped from the right side");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }else if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) < 2f){

            if(wasWarningIssued || ((currentLineNumber > preChangeLineNumber) && GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ) >= 0.99)){
                Debug.Log("Death Animation to the left");
            }else{ 
                if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) <= 0f){
                    OnWarningBounceBackToPreChangeLaneEvent(EventArgs.Empty);
                }
                Debug.Log("Player bumped from the left side");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }else{
            Debug.Log("lunch Ragdoll?");
        }
    }
}
