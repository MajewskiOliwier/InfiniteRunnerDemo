using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRendereSize : MonoBehaviour
{
    private void Start() {
        Vector3 size = GetComponent<Collider>().bounds.size;
        Debug.Log(this.gameObject.name + " : x = " +  size.x + " ,y = " +  size.y + " ,z = " +  size.z);
    }
}
