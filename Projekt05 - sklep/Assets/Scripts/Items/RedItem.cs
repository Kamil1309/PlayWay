using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interact;

public class RedItem : Item
{
    public override void OnUse(){
        //OnUse declaration for red item;
    }

    public override void SetProperties(){
        _name = "Red Item";
        _description = "This is RED item, nice that you have it.";
        _weight = 10;
        _type = "normal";
        _price = 10;
    }

    private void Start() {
        SetProperties();
    }
}
