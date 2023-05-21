using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterJump : MonoBehaviour
{
    private MainCharacterMovement mainCharacterMovement;

    [SerializeField] float jumpHeight; //serialize field for easier debuging

    private void Awake() {
        mainCharacterMovement = gameObject.GetComponent<MainCharacterMovement>();
    }

    
}
