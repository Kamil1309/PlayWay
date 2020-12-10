using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inter;
using System;

public class Object01 : Interactable
{
    public override bool CanInteract(GameObject obj)
    {
        if(Math.Sqrt(Math.Pow(gameObject.transform.position.x - obj.transform.position.x, 2) + Math.Pow(gameObject.transform.position.y - obj.transform.position.y, 2) ) < 5)
        {
            return true;
        }
        else{
            return false;
        }
        
    }

    public override void Interact()
    {
        Debug.Log("You have interacted: " + gameObject.name);
    }

}
