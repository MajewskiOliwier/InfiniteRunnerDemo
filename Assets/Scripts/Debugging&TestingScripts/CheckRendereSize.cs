using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRendereSize : MonoBehaviour
{
    private void Start() {
        Vector3 size = GetComponent<Collider>().bounds.size;
        Debug.Log("x = " +  size.x);
        Debug.Log("y = " +  size.y);
        Debug.Log("z = " +  size.z);
    }
}
