using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterManager : MonoBehaviour
{
    private Animator mainCharacterAnimator;
    
    private void Awake(){
        mainCharacterAnimator = gameObject.GetComponent<Animator>();
    }


    public Animator getMainCharacterAnimator(){
        return mainCharacterAnimator;
    }
}
