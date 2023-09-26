using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Obstacle
{
    //Lamp size x = 10 (uneven) , y = 10, z = 5 (uneven)

    private void Awake(){
        this.setCurrentLine(6); // for debbuging
    }



    protected override void checkHitType(Collision collision){
        setCurrentLine(6);
        Vector3 contactPoint = collision.contacts[0].point;
        GameObject player = collision.gameObject;

        float contactPointX = (collision.contacts[0].point.x + collision.contacts[1].point.x)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        float contactPointY = (collision.contacts[0].point.y + collision.contacts[1].point.y)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        float contactPointZ = (collision.contacts[0].point.z + collision.contacts[1].point.z)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        
        float pointOfContactOnYPos = 7.5f; // during debbuging point of contact on y pos was equal to : 7,518881
        float lampWidth = 2f/2f;
        bool wasWarningIssued  = player.GetComponent<MainCharacterAnimationSelector>().GetWarningIssued();
        
        Vector3 localCollisionPoint = transform.InverseTransformPoint(contactPoint);
        Debug.Log(this.gameObject.name + "local hit location with my function:  + " + GetLocalHitLocationInZPos(localCollisionPoint.z,contactPointZ ));
        
        Debug.Log(this.gameObject.name + " : collision in x dimension : " + contactPointX + " ,collision in y dimension : " + contactPointY + " Local x pos = " + GetLocalHitLocationInXPos(lampWidth,contactPointX));


        if(contactPointY > pointOfContactOnYPos){
            OnFinalCollisionEvent(EventArgs.Empty);
        }else{
            if(wasWarningIssued){
                Debug.Log("Death Animation by running into lamp");
            }else{ 
                int preChangeLineNr = player.GetComponent<MainCharacterChangeLanes>().GetPreChangePosition();
                int currentLineNr = player.GetComponent<MainCharacterChangeLanes>().GetCurrentLineXPosition();
                Debug.Log("Prechange nr = "+preChangeLineNr + " CurrentLineNr = "+currentLineNr);
                if(GetLocalHitLocationInXPos(lampWidth,contactPointX) >= 1f && (preChangeLineNr > currentLineNr)){
                    OnWarningBounceBackToPreChangeLaneEvent(EventArgs.Empty);
                    Debug.Log("LocalXpos >= 1f :");
                }else if(GetLocalHitLocationInXPos(lampWidth,contactPointX) <= 0 && (preChangeLineNr < currentLineNr)){
                    OnWarningBounceBackToPreChangeLaneEvent(EventArgs.Empty);
                    Debug.Log("LocalXpos <= 0f");
                }else{

                    Debug.Log("Player run into the pole but continue changing lane");
                }

                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }
    }
}
