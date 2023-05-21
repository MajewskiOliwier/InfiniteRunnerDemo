using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterMovement : MonoBehaviour{
    private enum Line{
        Left,
        Middle,
        Right
    }


    private Rigidbody MCrigidbody;
    [SerializeField] private InputActionReference movementLeft, movementRight;
    private Vector2 moveInput;

    

    [SerializeField] private float changeLaneSpeed;
    [SerializeField] private float straightLaneSpeed;

    [SerializeField] private bool busy = false; //serialize field for easier debuging
    [SerializeField] private bool changeToTheRight = false; //serialize field for easier debuging
    [SerializeField] private float changeLaneStartPosition; //serialize field for easier debuging
    [SerializeField] private Line currentLine = Line.Middle; //serialize field for easier debuging

    private void Awake() {
        MCrigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        movementLeft.action.performed += PerformMoveToLeft;
        movementRight.action.performed += PerformMoveToRight;
    }

    private void OnDisable() { //I won't be disabling MainCharacter but it should stay here just in case 
        movementLeft.action.performed -= PerformMoveToLeft;
        movementRight.action.performed -= PerformMoveToRight;
    }

    private void PerformMoveToRight(InputAction.CallbackContext obj){ // add blockade when moving to the right on right track
        Debug.Log("ruch w prawo");
        if(busy == false){
            changeLaneStartPosition = transform.position.x;
            MCrigidbody.velocity = (new Vector3(changeLaneSpeed,0,MCrigidbody.velocity.z)); 
            // Debug.Log("Turn to the Left. Turn speed = "+changeLaneSpeed+" Straight movement speed = "+straightLaneSpeed);
            // Debug.Log(" Vector3 = "+(new Vector3(changeLaneSpeed*Time.deltaTime,0,0)) + (new Vector3(0,0,straightLaneSpeed*Time.deltaTime)));
            // Debug.Log("StartPosition : " + changeLaneStartPosition);
            Debug.Log(MCrigidbody.velocity);
            busy = true;
            changeToTheRight = true;
            currentLine += 1;
        }
    }

    private void PerformMoveToLeft(InputAction.CallbackContext obj){
        Debug.Log("ruch w lewo");
        if(busy == false){
            changeLaneStartPosition = transform.position.x;
            MCrigidbody.velocity = (new Vector3((-1)*changeLaneSpeed,0,MCrigidbody.velocity.z));
            // Debug.Log("Turn to the Right. Turn speed = " + changeLaneSpeed + " Straight movement speed = "+straightLaneSpeed);
            // Debug.Log(" Vector3 = " + (new Vector3((-1)*changeLaneSpeed*Time.deltaTime,0,0)) + (new Vector3(0,0,straightLaneSpeed*Time.deltaTime)));
            // Debug.Log("StartPosition : " + changeLaneStartPosition);
            Debug.Log(MCrigidbody.velocity);
            busy = true;
            changeToTheRight = false;
            currentLine -= 1;
        }
    }

    

    private void Start() {
        MCrigidbody.velocity = new Vector3(MCrigidbody.velocity.x, MCrigidbody.velocity.y,straightLaneSpeed);
    }

    void LateUpdate(){
        //MCrigidbody.AddForce(new Vector3(0,0,1)); //delete
        if(busy){
            ComparePositions(changeLaneStartPosition,changeToTheRight);
        }else{
            MCrigidbody.velocity = new Vector3(0 , 0,straightLaneSpeed);
        }
        Debug.Log(currentLine);
    }

    private void ComparePositions(float startingPos,bool isMovingToTheRight){
        if(isMovingToTheRight){
            //Debug.Log("startPos = "+(startingPos+12) + "currPos = "+transform.position.x);
            if(startingPos+12 < transform.position.x){
                MCrigidbody.velocity = new Vector3(0 , 0,straightLaneSpeed);
                busy = false;

                fixOffset(currentLine);
            }
        }else{
            if(startingPos-12 > transform.position.x){
                MCrigidbody.velocity = new Vector3(0 , 0,straightLaneSpeed);
                busy = false;

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

    public bool GetBusy(){
        return busy;
    }
}

