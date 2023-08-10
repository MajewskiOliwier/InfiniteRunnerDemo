using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour{
  


    private Rigidbody mCrigidbody;
    private Vector2 moveInput;
    private MainCharacterChangeLanes mainCharacterChangeLanes; 
    


    [SerializeField] private float straightLaneSpeed;
    [SerializeField] private bool busy = false; //serialize field for easier debuging
    

    private void Awake() {
        mCrigidbody = gameObject.GetComponent<Rigidbody>();
        mainCharacterChangeLanes = gameObject.GetComponent<MainCharacterChangeLanes>();
    }

    

    

    private void Start() {
        mCrigidbody.velocity = new Vector3(mCrigidbody.velocity.x, mCrigidbody.velocity.y,straightLaneSpeed);
    }

    void LateUpdate(){
        //mCrigidbody.AddForce(new Vector3(0,0,1)); //delete
        if(busy){
            mainCharacterChangeLanes.ComparePositions(straightLaneSpeed,mainCharacterChangeLanes.GetChangeLaneStartPosition(),mainCharacterChangeLanes.GetChangeToRight());
        }else{
            mCrigidbody.velocity = new Vector3(0 , 0,straightLaneSpeed);
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

