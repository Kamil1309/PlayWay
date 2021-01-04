using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenItem : Item
{
    public override void OnUse(){
        //OnUse declaration for green item;
    }

    public override void SetProperties(){
        _name = "Green Item";
        _description = "This is GREEN item, nice that you have it.";
        _weight = 5;
        _type = "unique";
        _price = 50;
    }

    private void Start() {
        SetProperties();
    }
}
