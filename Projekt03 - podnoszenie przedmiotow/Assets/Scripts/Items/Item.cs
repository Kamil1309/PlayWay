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

    float angleMultiplier = 2 * Mathf.PI/360.0f;
    float angleShift = Mathf.PI * 1/2.0f;
    float acceptanceAngle = 30/360.0f * Mathf.PI;

    public virtual void OnUse(){}

    public bool CanInteract(GameObject player)
    {   
        if(gameObject.activeSelf)
        {
            float playerDirRad = player.transform.localEulerAngles.y * angleMultiplier;
            playerDirRad = -playerDirRad + angleShift;

            Vector2 itemPlayer = new Vector2(gameObject.transform.position.x - player.gameObject.transform.position.x, 
                                            gameObject.transform.position.z - player.gameObject.transform.position.z);
            Vector2 playerDirection = new Vector2(Mathf.Cos(playerDirRad), Mathf.Sin(playerDirRad));

            if( Vector2.Dot(itemPlayer, playerDirection)/(itemPlayer.magnitude * playerDirection.magnitude) > 
                                        Mathf.Cos(acceptanceAngle)){
                return true;
            }
        }
        return false;
    }

    public void Interact(GameObject player){ 
        if(player.GetComponent<InventorySystem>() != null){
            player.GetComponent<InventorySystem>().Items.Add(gameObject);
            player.GetComponent<InventorySystem>().currentCapacity += _weight;
            gameObject.SetActive(false);
        }
        else{
            Debug.LogError("No component 'InventorySystem'!!!");
        }
    }

    public bool CheckCapacityLimit(GameObject player){
        if(player.GetComponent<InventorySystem>() != null){
            if(player.GetComponent<InventorySystem>().currentCapacity + _weight <= player.GetComponent<InventorySystem>().maxCapacity){
                return true;
            }
            else{
                Debug.Log("Weight too big!!! You can't pick it up :(");
                return false;   
            }
        }
        else{
            Debug.LogError("No component 'InventorySystem'!!!");
            return false;
        }
    }
}
