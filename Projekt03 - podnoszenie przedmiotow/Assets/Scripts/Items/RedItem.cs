using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interact;

public class RedItem : Item
{
    public override void OnUse(){
        //OnUse declaration for red item;
    }

    private void Start() {
        _name = "Red Item";
        _description = "This is RED item, nice that you have it.";
        _weight = 10.0f;
    }
}
