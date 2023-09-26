using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterChangeLanes : MonoBehaviour{
    
    private enum Line{
        Left = -1,
        Middle = 0,
        Right = 1
    }

    [SerializeField] private Line currentLine = Line.Middle; //serialize field for easier debuging
    [SerializeField] private Line preChangeLine = Line.Middle; //serialize field for easier debuging
    
    [SerializeField] private InputActionReference movementLeft, movementRight;
    [SerializeField] private float changeLaneSpeed;

    [SerializeField] private bool changeToTheRight = false; //serialize field for easier debuging
    [SerializeField] private float changeLaneStartPosition; //serialize field for easier debuging

    private Rigidbody MCrigidbody;
    private MainCharacterMovement mainCharacterMovementManager;

    private void Awake(){
        mainCharacterMovementManager = gameObject.GetComponent<MainCharacterMovement>();
    }

    private void OnEnable() {
        movementLeft.action.performed += PerformMoveToLeft;
        movementRight.action.performed += PerformMoveToRight;
        Obstacle.OnWarningBounceBackToPreChangeLane += Obstacle_ReturnToPreviousLane;
    }

    private void OnDisable() { //I won't be disabling MainCharacter but it should stay here just in case 
        movementLeft.action.performed -= PerformMoveToLeft;
        movementRight.action.performed -= PerformMoveToRight;
        Obstacle.OnWarningBounceBackToPreChangeLane -= Obstacle_ReturnToPreviousLane;
    }

    private void Start(){
        MCrigidbody = mainCharacterMovementManager.GetmCRigidBody();
    }

    private void PerformMoveToRight(InputAction.CallbackContext obj){
        
        Animator mainCharacterAnimator = gameObject.GetComponent<MainCharacterManager>().getMainCharacterAnimator();

        if(mainCharacterMovementManager.GetBusy() == false && currentLine != Line.Right){
            changeLaneStartPosition = transform.position.x;
            MCrigidbody.velocity = (new Vector3(changeLaneSpeed,-15,MCrigidbody.velocity.z)); 
            // Debug.Log("Turn to the Left. Turn speed = "+changeLaneSpeed+" Straight movement speed = "+straightLaneSpeed);
            // Debug.Log(" Vector3 = "+(new Vector3(changeLaneSpeed*Time.deltaTime,0,0)) + (new Vector3(0,0,straightLaneSpeed*Time.deltaTime)));
            // Debug.Log("StartPosition : " + changeLaneStartPosition);
            //Debug.Log(MCrigidbody.velocity);
            mainCharacterMovementManager.SetBusy(true);
            changeToTheRight = true;
            currentLine += 1;
            mainCharacterAnimator.SetInteger("RunTypes",1);
        }
    }

    private void PerformMoveToLeft(InputAction.CallbackContext obj){

        Animator mainCharacterAnimator = gameObject.GetComponent<MainCharacterManager>().getMainCharacterAnimator();
        
        if(mainCharacterMovementManager.GetBusy() == false && currentLine != Line.Left){
            changeLaneStartPosition = transform.position.x;
            MCrigidbody.velocity = (new Vector3((-1)*changeLaneSpeed,-15,MCrigidbody.velocity.z));
            // Debug.Log("Turn to the Right. Turn speed = " + changeLaneSpeed + " Straight movement speed = "+straightLaneSpeed);
            // Debug.Log(" Vector3 = " + (new Vector3((-1)*changeLaneSpeed*Time.deltaTime,0,0)) + (new Vector3(0,0,straightLaneSpeed*Time.deltaTime)));
            // Debug.Log("StartPosition : " + changeLaneStartPosition);
            //Debug.Log(MCrigidbody.velocity);
            mainCharacterMovementManager.SetBusy(true);
            changeToTheRight = false;
            currentLine -= 1;
            mainCharacterAnimator.SetInteger("RunTypes",-1);
        }
    }

    private void Obstacle_ReturnToPreviousLane(object sender, EventArgs e){
        changeLaneStartPosition = (int)currentLine*12f;
        Animator mainCharacterAnimator = gameObject.GetComponent<MainCharacterManager>().getMainCharacterAnimator();
        
        if(currentLine > preChangeLine){
            MCrigidbody.velocity = (new Vector3((-1)*changeLaneSpeed,-15,MCrigidbody.velocity.z));
            
            mainCharacterMovementManager.SetBusy(true);
            changeToTheRight = false;
            currentLine -= 1;
            mainCharacterAnimator.SetInteger("RunTypes",-1);
        }else{
            MCrigidbody.velocity = (new Vector3(changeLaneSpeed,MCrigidbody.velocity.y,MCrigidbody.velocity.z)); 
            
            mainCharacterMovementManager.SetBusy(true);
            changeToTheRight = true;
            currentLine += 1;
            mainCharacterAnimator.SetInteger("RunTypes",1);
        }
    }

    public void ComparePositions(float straightLaneSpeed){
        //float startingPos = changeLaneStartPosition;
        if(MCrigidbody.velocity.y != 0 && gameObject.GetComponent<MainCharacterJump>().GetIsJumping() == true){
            return ;
        }

        Animator mainCharacterAnimator = gameObject.GetComponent<MainCharacterManager>().getMainCharacterAnimator();
        

        if(changeToTheRight){ 
            if(changeLaneStartPosition+12 < transform.position.x){  //changeLanePosition is starting position in this context
                MCrigidbody.velocity = new Vector3(0 , MCrigidbody.velocity.y,straightLaneSpeed);
                mainCharacterMovementManager.SetBusy(false);
                
                mainCharacterAnimator.SetInteger("RunTypes",0);
                fixOffset(currentLine);
            }
        }else{
            if(changeLaneStartPosition-12 > transform.position.x){
                MCrigidbody.velocity = new Vector3(0 , MCrigidbody.velocity.y,straightLaneSpeed);
                mainCharacterMovementManager.SetBusy(false);

                mainCharacterAnimator.SetInteger("RunTypes",0);
                fixOffset(currentLine);
            }
        }
    }

    private void fixOffset(Line currentLineToRemoveOffest){
        switch(currentLineToRemoveOffest){
            case Line.Left:{
                if(transform.position.x != -12){
                    transform.position = new Vector3( -12, transform.position.y, transform.position.z);
                }
                break;
            }
            case Line.Middle:{
                if(transform.position.x != 0){
                    transform.position = new Vector3(0, transform.position.y, transform.position.z);
                }
                break;
            }
            case Line.Right:{
                if(transform.position.x != 12){
                    transform.position = new Vector3(12, transform.position.y, transform.position.z);
                }
                break;
            }
        }
        preChangeLine = currentLine;
    }

    public bool GetChangeToRight(){
        return changeToTheRight;
    }

    public float GetChangeLaneStartPosition(){
        return changeLaneStartPosition;
    }
    
    public int GetCurrentLineXPosition(){
        //Debug.Log("returned : " + (int)currentLine);
        return (int)currentLine;
    }
    
    public int GetPreChangePosition(){
        return (int)preChangeLine;
    }
}
