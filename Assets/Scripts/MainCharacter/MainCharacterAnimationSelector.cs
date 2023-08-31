using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharacterAnimationSelector : MonoBehaviour
{
    [SerializeField] private enum HitXLocation { Left, Mid, Right, None};

    public void CheckHitLocationX(Transform transform){
        Debug.Log("Hej");
    }
    
}
