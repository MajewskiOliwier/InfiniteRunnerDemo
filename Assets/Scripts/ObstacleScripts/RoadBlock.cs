using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : Obstacle
{
    //RoadBlock size x = 10, y = 4, z = 3;
    protected override void checkHitType(Collision collision){
        Vector3 contactPoint = collision.contacts[0].point;
        GameObject player = collision.gameObject;

        float contactPointX = (collision.contacts[0].point.x + collision.contacts[1].point.x)/2f; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        float contactPointY = collision.contacts[0].point.y; // this avarage gets centre of the hit location between MainCharacter and Obstacle
        //Debug.Log("collision in x dimension " + contactPointX);
        //Debug.Log("collision in y dimension " + contactPointY);

        float roadblockWidth = 10f/2f;
        bool wasWarningIssued  = player.GetComponent<MainCharacterAnimationSelector>().GetWarningIssued();

        int currentLineNumber = player.GetComponent<MainCharacterChangeLanes>().GetCurrentLineXPosition();
        int preChangeLineNumber = player.GetComponent<MainCharacterChangeLanes>().GetPreChangePosition();
        
        if(contactPointY >= 3f){
            if(wasWarningIssued){
                Debug.Log("Death Animation by tipping over ");
            }else{ 
                Debug.Log("Player tipped over but regain balance");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }else if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) >= 10f){ //occurs when bumping to the side 
            
            if(wasWarningIssued || (currentLineNumber < preChangeLineNumber)){ //if the (int) currentLine is lower than preChangeLine then it means that the collision occured after changing lane to the left into the ,,warning zone" 
                Debug.Log("Death Animation to the right");
            }else{ 
                Debug.Log("Player bumped from the right side");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }else if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) <= 0f){

            if(wasWarningIssued || (currentLineNumber > preChangeLineNumber)){
                Debug.Log("Death Animation to the left");
            }else{ 
                Debug.Log("Player bumped from the left side");
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
        }else if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) > 8f){
            //check if Warrning was issued
            //if yes then play death animation
            //otherwiser play Strafe
            if(wasWarningIssued){
                Debug.Log("Death Animation to the right");
            }else{ 
                Debug.Log("Play animation strafe right "+ (roadblockWidth + (contactPointX-currentLine)));
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
            
            //Debug.Log(roadblockWidth + (contactPointX-currentLine));
        }else if(GetLocalHitLocationInXPos(roadblockWidth,contactPointX) < 2f){
            
            if(wasWarningIssued){
                Debug.Log("Death Animation to the left");
            }else{ 
                Debug.Log("Play animation strafe left  "+ (roadblockWidth + (contactPointX-currentLine)));
                player.GetComponent<MainCharacterAnimationSelector>().SetWarningIssued();
            }
            //Debug.Log(contactPointX-currentLine));
        }else{
            Debug.Log("Play Death animation hit dead centre");
        }
    }

    private float GetLocalHitLocationInXPos(float width, float contactPointXWorldPos){
        return (width + (contactPointXWorldPos-currentLine));
    }
}
