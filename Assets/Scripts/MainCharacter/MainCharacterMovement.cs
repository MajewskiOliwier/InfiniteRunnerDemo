using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour{
  


    private Rigidbody mCrigidbody;
    private Vector2 moveInput;
    private MainCharacterChangeLanes mainCharacterChangeLanes; 
    private MainCharacterJump mainCharacterJump; 
    


    [SerializeField] private float straightLaneSpeed;
    [SerializeField] private bool busy = false; //serialize field for easier debuging
    

    private void Awake() {
        mCrigidbody = gameObject.GetComponent<Rigidbody>();
        mainCharacterChangeLanes = gameObject.GetComponent<MainCharacterChangeLanes>();
        mainCharacterJump = gameObject.GetComponent<MainCharacterJump>();
    }

    

    

    private void Start() {
        mCrigidbody.velocity = new Vector3(mCrigidbody.velocity.x, mCrigidbody.velocity.y,straightLaneSpeed);
    }

    void Update(){
        //mCrigidbody.AddForce(new Vector3(0,0,1)); //delete
        if(busy){
            mainCharacterChangeLanes.ComparePositions(straightLaneSpeed);
            mainCharacterJump.ComparePositions();
        }else{
            mCrigidbody.velocity = new Vector3(0 , -mainCharacterJump.GetJumpingVelocity(),straightLaneSpeed);
        }
        
    }

    

    public bool GetBusy(){
        return busy;
    }
    

    public void SetBusy(bool currentState){
        busy = currentState;
    }

    public Rigidbody GetmCRigidBody(){
        return mCrigidbody;
    }

    public float GetStraightLaneSpeed(){
        return straightLaneSpeed;
    }
    
}

