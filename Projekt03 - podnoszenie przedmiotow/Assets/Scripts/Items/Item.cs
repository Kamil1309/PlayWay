using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Interact;
public class Item : MonoBehaviour, IInteractable
{
    public string _name;
    public string _description;
    public float _weight;

    public Texture2D inventorySprite;

    public virtual void OnUse(){}

    public bool CanInteract(GameObject player)
    {   
        if(gameObject.activeSelf)
        {
            float playerDirRad = player.transform.localEulerAngles.y/360.0f * 2 * Mathf.PI;
            playerDirRad = -playerDirRad + Mathf.PI * 1/2.0f;

            Vector2 itemPlayer = new Vector2(gameObject.transform.position.x - player.gameObject.transform.position.x, gameObject.transform.position.z - player.gameObject.transform.position.z);
            Vector2 playerDirection = new Vector2(Mathf.Cos(playerDirRad), Mathf.Sin(playerDirRad));

            if( Vector2.Dot(itemPlayer, playerDirection)/(itemPlayer.magnitude * playerDirection.magnitude) > Mathf.Cos(30/360.0f * Mathf.PI)){
                return true;
            }
        }
        return false;
    }

    public void Interact(GameObject player){ 
        player.GetComponent<InventorySystem>().Items.Add(gameObject);
        player.GetComponent<InventorySystem>().currentCapacity += _weight;
        gameObject.SetActive(false);
    }

    public bool CheckCapacityLimit(GameObject player){
        if(player.GetComponent<InventorySystem>().currentCapacity + _weight <= player.GetComponent<InventorySystem>().maxCapacity){
            return true;
        }
        else{
            Debug.Log("Weight too big!!! You can't pick it up :(");
            return false;   
        }
    }
}
