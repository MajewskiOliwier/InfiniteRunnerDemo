using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharacterAnimationSelector : MonoBehaviour
{
    [SerializeField] private bool warningIssued = false; 
    //if you bump into object to the side or barely (~2f) missed obstacle during changing lane issue warning
    //if the warning was issued in the last X seconds play death animation instead



    public void CheckHitLocationX(Transform transform){
        Debug.Log("Hej");
    }

    public void SetWarningIssued(){
        warningIssued = true;
        float warningTimeLimit = 5f;
        Debug.Log("warningIssued was changed to : "+warningIssued);
        StartCoroutine(WaitToResetWarning(warningTimeLimit));
    }

    private IEnumerator WaitToResetWarning(float waitTime){
        yield return new WaitForSeconds(waitTime);
        warningIssued = false;
    }

    public bool GetWarningIssued(){
        return warningIssued;
    }
}
