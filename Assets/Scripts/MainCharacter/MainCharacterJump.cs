using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterJump : MonoBehaviour
{


    [SerializeField] float jumpVelocity = 15f; //serialize field for easier debuging
    [SerializeField] float jumpDuration = 0.4f; //serialize field for easier debuging


    [SerializeField] private InputActionReference jumpActionInputReference;
    [SerializeField] private float jumpStartPosition; //serialize field for easier debuging
    [SerializeField] private bool isJumping;

    private Rigidbody MCrigidbody;
    private MainCharacterMovement mainCharacterMovement;

    private void Awake() {
        mainCharacterMovement = gameObject.GetComponent<MainCharacterMovement>();
        MCrigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable(){
        jumpActionInputReference.action.performed += PerformJump;
    }

    private void OnDisable(){   
        jumpActionInputReference.action.performed -= PerformJump; //added in case where MainCharacter is removed
    }

    private void PerformJump(InputAction.CallbackContext obj){
        Debug.Log("Perform jump");
        if(mainCharacterMovement.GetBusy() == false){

            gameObject.GetComponent<MainCharacterManager>().getMainCharacterAnimator().SetTrigger("IsJumping");
            mainCharacterMovement.SetBusy(true);
            
            Debug.Log("Jump successful");
            jumpStartPosition = transform.position.y;
            
            StartCoroutine(Countdown());
            
            //MCrigidbody.velocity = (new Vector3((-1)*changeLaneSpeed,0,MCrigidbody.velocity.z));

        }
    }

    


    public void ComparePositions(){
        if(isJumping == false){
            return ;
        }


        if(jumpStartPosition > transform.position.y){  //changeLanePosition is starting position in this context
                Debug.Log("End of jump");
                transform.position = new Vector3(transform.position.x, 3, transform.position.z);
                mainCharacterMovement.SetBusy(false);
                isJumping = false;
        }else{
            MCrigidbody.velocity = (new Vector3(0,-jumpVelocity,MCrigidbody.velocity.z));
        }
    }

    private IEnumerator Countdown(){
        MCrigidbody.velocity = (new Vector3(0,jumpVelocity,MCrigidbody.velocity.z));
        
        yield return new WaitForSeconds(jumpDuration);
        Debug.Log(this.transform.position.y);
        isJumping = true;
        //MCrigidbody.velocity = (new Vector3(0,-jumpVelocity,MCrigidbody.velocity.z));
        mainCharacterMovement.SetBusy(true);
    }

    public void SetIsJumping(bool isJumpingCurrently){
        isJumping = isJumpingCurrently;
    }

    public bool GetIsJumping(){
        return isJumping;
    }

    public float GetJumpingVelocity(){
        return jumpVelocity;
    }
}
