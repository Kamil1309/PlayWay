using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackItem : Item
{
    public override void OnUse(){
        //OnUse declaration for black item;
    }

    public override void SetProperties(){
        _name = "Black Item";
        _description = "This is BLACK item, nice that you have it.";
        _weight = 100;
        _type = "legendary";
        _price = 100;
    }

    private void Start() {
        SetProperties();
    }


}
