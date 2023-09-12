using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainCharacterRoll : MonoBehaviour
{
    [SerializeField] float characterYPosDuringRoll = 0.8f; //serialize field for easier debuging
    [SerializeField] float characterYCenterPosDuringRoll = -0.6f; //serialize field for easier debuging
    [SerializeField] float characterYPosDefault = 2f; //serialize field for easier debuging
    [SerializeField] float characterYCenterPosDefault = 0f; //serialize field for easier debuging
    [SerializeField] float colliderSizeAdjustingSpeed = 3f; 
    [SerializeField] float colliderCentreAdjustingSpeed = 3f; 


    [SerializeField] private InputActionReference rollActionInputReference;
    
    
    private Rigidbody MCrigidbody;
    private MainCharacterMovement mainCharacterMovement;
    private BoxCollider mainCharacterCollider;

    private bool isRolling = false;
    private bool isRollingEnding = false;

    private void Awake() {
        mainCharacterMovement = gameObject.GetComponent<MainCharacterMovement>();
        MCrigidbody = gameObject.GetComponent<Rigidbody>();
        mainCharacterCollider = gameObject.GetComponent<BoxCollider>();
        
    }

    private void OnEnable(){
        rollActionInputReference.action.performed += PerformRoll;
    }

    private void OnDisable(){   
        rollActionInputReference.action.performed -= PerformRoll; //added in case where MainCharacter is removed
    }

    private void Update() {
        if(isRolling){
            if(isRollingEnding){
                IncreaseMainCharacterCollider();
            }else{
                DecreaseMainCharacterCollider();
            }
        }else{
            Debug.Log("Koniec");
        }
    }

    private void PerformRoll(InputAction.CallbackContext obj){
        Debug.Log("Perform roll");
        
        if(mainCharacterMovement.GetBusy() == false){

            gameObject.GetComponent<MainCharacterManager>().getMainCharacterAnimator().SetTrigger("isRolling");
            
            isRolling = true;
        }
    }

    private void adjustMainCharacterCollider(bool increaseColliderSize){
        float targetColliderSize;
        if(increaseColliderSize){
            targetColliderSize = characterYPosDefault;
        }else{
            targetColliderSize = characterYPosDuringRoll;
        }
        
        Vector3 colliderSize = mainCharacterCollider.size;
        Debug.Log("targetSize " + targetColliderSize);

        if(colliderSize.y > targetColliderSize){
            Debug.Log(mainCharacterCollider.size);
            colliderSize += Vector3.up * colliderSizeAdjustingSpeed * Time.deltaTime;
            if(colliderSize.y < targetColliderSize){
                colliderSize = new Vector3(mainCharacterCollider.size.x,characterYPosDuringRoll,mainCharacterCollider.size.z);
            }
            mainCharacterCollider.size =  colliderSize;
        }
    }

    private void DecreaseMainCharacterCollider(){
        Vector3 colliderSize = mainCharacterCollider.size;
        Vector3 colliderOffset= mainCharacterCollider.center;
        if(colliderSize.y > characterYPosDuringRoll){
            colliderSize += Vector3.down * colliderSizeAdjustingSpeed * Time.deltaTime;
            colliderOffset -= Vector3.down * colliderCentreAdjustingSpeed * Time.deltaTime;
            if(colliderSize.y < characterYPosDuringRoll){
                colliderSize = new Vector3(mainCharacterCollider.size.x,characterYPosDuringRoll,mainCharacterCollider.size.z);
                colliderOffset = new Vector3(mainCharacterCollider.center.x,characterYCenterPosDuringRoll,mainCharacterCollider.center.z);
                isRollingEnding = true;
                Debug.Log("Finished Decreasing");
            }
            mainCharacterCollider.size =  colliderSize;
            mainCharacterCollider.center =  colliderOffset;
            transform.position = new Vector3(12f*gameObject.GetComponent<MainCharacterChangeLanes>().GetCurrentLineXPosition(),3,transform.position.z);
        }
    }

    private void IncreaseMainCharacterCollider(){
        Vector3 colliderSize = mainCharacterCollider.size;
        Vector3 colliderOffset= mainCharacterCollider.center;
        if(colliderSize.y < characterYPosDefault){
            colliderSize += Vector3.up * colliderSizeAdjustingSpeed * Time.deltaTime;
            colliderOffset += Vector3.up * colliderCentreAdjustingSpeed * Time.deltaTime;
            if(colliderSize.y > characterYPosDefault){
                colliderSize = new Vector3(mainCharacterCollider.size.x,characterYPosDefault,mainCharacterCollider.size.z);
                colliderOffset = new Vector3(mainCharacterCollider.center.x,characterYCenterPosDefault,mainCharacterCollider.center.z);
                isRollingEnding = false;
                isRolling = false;
                
                
                Debug.Log("Finished Increasing");
            }
            mainCharacterCollider.size =  colliderSize;
            mainCharacterCollider.center =  colliderOffset;
            transform.position = new Vector3(12f*gameObject.GetComponent<MainCharacterChangeLanes>().GetCurrentLineXPosition(),3,transform.position.z);
        }
    }
}
