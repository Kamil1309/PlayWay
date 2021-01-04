using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowItem : Item
{
    public override void OnUse(){
        //OnUse declaration for yellow item;
    }

    public override void SetProperties(){
        _name = "Yellow Item";
        _description = "This is YELLOW item, nice that you have it.";
        _weight = 20;
        _type = "normal";
        _price = 5;
    }

    private void Start() {
        SetProperties();
    }
}
