using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private enum Line{
        Left = -12,
        Middle = 0,
        Right = 12
    }

    protected float itemLocationLine = (int)Line.Middle;

    public void setCurrentLine(int itemLocationLineNumber){
        itemLocationLine = itemLocationLineNumber;
    }

    protected abstract void PickUpItem();
    protected abstract void ItemAnimation();

    private void OnTriggerEnter(Collider collider) {
        if(collider.transform.tag == "Player"){
            PickUpItem();
        }
    }

}
