using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Interact;

public class Item : MonoBehaviour, IInteractable
{
    public string _name;
    public string _description;
    public int _weight;
    public string _type;
    public int _price;

    public Texture2D inventorySprite;

    float acceptanceAngle = 30.0f;

    public virtual void OnUse(){}

    public virtual void SetProperties(){}

    public bool CanInteract(GameObject player)
    {   
        if(gameObject.activeSelf)
        {
            Vector3 playerForward = player.transform.forward;
            Vector3 itemPlayerVec = gameObject.transform.position - player.transform.position;

            if( Vector3.Angle(playerForward, itemPlayerVec) < acceptanceAngle )
                return true;
        }
        return false;
    }

    public void Interact(GameObject player){ 
        if(player.GetComponent<Inventory>() != null){
            player.GetComponent<Inventory>().Items.Add(gameObject);
            player.GetComponent<Inventory>().currentCapacity += _weight;
            gameObject.SetActive(false);
        }
        else{
            Debug.LogError("No component 'Inventory'!!!");
        }
    }
}