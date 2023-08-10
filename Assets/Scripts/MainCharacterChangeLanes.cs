using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterChangeLanes : MonoBehaviour{
    
    private enum Line{
        Left,
        Middle,
        Right
    }

    [SerializeField] private Line currentLine = Line.Middle; //serialize field for easier debuging
    
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
    }

    private void OnDisable() { //I won't be disabling MainCharacter but it should stay here just in case 
        movementLeft.action.performed -= PerformMoveToLeft;
        movementRight.action.performed -= PerformMoveToRight;
    }

    private void Start(){
        MCrigidbody = mainCharacterMovementManager.GetmCRigidBody();
    }

    private void PerformMoveToRight(InputAction.CallbackContext obj){ // add blockade when moving to the right on right track
        Debug.Log("ruch w prawo");
        
        if(mainCharacterMovementManager.GetBusy() == false && currentLine != Line.Right){
            changeLaneStartPosition = transform.position.x;
            MCrigidbody.velocity = (new Vector3(changeLaneSpeed,0,MCrigidbody.velocity.z)); 
            // Debug.Log("Turn to the Left. Turn speed = "+changeLaneSpeed+" Straight movement speed = "+straightLaneSpeed);
            // Debug.Log(" Vector3 = "+(new Vector3(changeLaneSpeed*Time.deltaTime,0,0)) + (new Vector3(0,0,straightLaneSpeed*Time.deltaTime)));
            // Debug.Log("StartPosition : " + changeLaneStartPosition);
            Debug.Log(MCrigidbody.velocity);
            mainCharacterMovementManager.SetBusy(true);
            changeToTheRight = true;
            currentLine += 1;
        }
    }

    private void PerformMoveToLeft(InputAction.CallbackContext obj){
        Debug.Log("ruch w lewo");
        if(mainCharacterMovementManager.GetBusy() == false && currentLine != Line.Left){
            Debug.Log("move to left");
            changeLaneStartPosition = transform.position.x;
            MCrigidbody.velocity = (new Vector3((-1)*changeLaneSpeed,0,MCrigidbody.velocity.z));
            // Debug.Log("Turn to the Right. Turn speed = " + changeLaneSpeed + " Straight movement speed = "+straightLaneSpeed);
            // Debug.Log(" Vector3 = " + (new Vector3((-1)*changeLaneSpeed*Time.deltaTime,0,0)) + (new Vector3(0,0,straightLaneSpeed*Time.deltaTime)));
            // Debug.Log("StartPosition : " + changeLaneStartPosition);
            Debug.Log(MCrigidbody.velocity);
            mainCharacterMovementManager.SetBusy(true);
            changeToTheRight = false;
            currentLine -= 1;
        }
    }

    /*
    private void PerformMove(InputAction.CallbackContext obj,bool changeToRight){
        Debug.Log("ruch w lewo");
        if(busyState == false){
            changeLaneStartPosition = transform.position.x;
            MCrigidbody.velocity = (new Vector3((-1)*changeLaneSpeed,0,MCrigidbody.velocity.z));
            // Debug.Log("Turn to the Right. Turn speed = " + changeLaneSpeed + " Straight movement speed = "+straightLaneSpeed);
            // Debug.Log(" Vector3 = " + (new Vector3((-1)*changeLaneSpeed*Time.deltaTime,0,0)) + (new Vector3(0,0,straightLaneSpeed*Time.deltaTime)));
            // Debug.Log("StartPosition : " + changeLaneStartPosition);
            Debug.Log(MCrigidbody.velocity);
            busyState = true;
            changeToTheRight = false;
            currentLine -= 1;
        }
    }
    */

    public void ComparePositions(float straightLaneSpeed,float startingPos,bool isMovingToTheRight){
        //float startingPos = changeLaneStartPosition;
        //bool isMovingToTheRight = changeToTheRight;
        if(isMovingToTheRight){
            //Debug.Log("startPos = "+(startingPos+12) + "currPos = "+transform.position.x);
            if(startingPos+12 < transform.position.x){
                MCrigidbody.velocity = new Vector3(0 , 0,straightLaneSpeed);
                mainCharacterMovementManager.SetBusy(false);

                fixOffset(currentLine);
            }
        }else{
            if(startingPos-12 > transform.position.x){
                MCrigidbody.velocity = new Vector3(0 , 0,straightLaneSpeed);
                mainCharacterMovementManager.SetBusy(false);

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

    }

    private void LateUpdate(){   // for debuging purpose
        Debug.Log(currentLine);
    }

    public bool GetChangeToRight(){
        return changeToTheRight;
    }

    public float GetChangeLaneStartPosition(){
        return changeLaneStartPosition;
    }
}
