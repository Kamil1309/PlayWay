using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interact;

public class PlayerInteractionComponent : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> nearObjects = new List<GameObject>();

    private GUIStyle guiStyle = new GUIStyle();

    public void InteractWithInteractable(){

        foreach(GameObject obj in nearObjects){
            if(obj.tag == "Item" && obj.GetComponent<Item>() != null){
                if( obj.GetComponent<Item>().CanInteract(gameObject) )
                {
                    if(CheckCapacityLimit(obj)){
                        obj.GetComponent<Item>().Interact(gameObject);
                        break;
                    }
                }
            }

            if(obj.tag == "Dealer" && obj.GetComponent<Dealer>() != null){
                if( obj.GetComponent<Dealer>().CanInteract(gameObject) )
                {
                    obj.GetComponent<Dealer>().Interact(gameObject);
                    break;
                }
            }
        }
        nearObjects.RemoveAll( obj => obj.activeSelf == false);
    }

    public bool CheckCapacityLimit(GameObject item){
        if( GetComponent<Inventory>() != null){
            if(GetComponent<Inventory>().currentCapacity + item.GetComponent<Item>()._weight <= GetComponent<Inventory>().maxCapacity){
                return true;
            }
            else{
                Debug.Log("Weight too big!!! You can't pick it up :(");
                return false;   
            }
        }
        else
        {
            Debug.Log("There is no inventory");
            return false;
        }
    }

    private void OnGUI() {
        if(!GetComponent<Inventory>().isDuringTrade){
            foreach(GameObject obj in nearObjects){
                if( obj.GetComponent<IInteractable>().CanInteract(gameObject) )
                {
                    guiStyle.fontSize = 40;
                    GUI.contentColor = Color.black;
                    if(obj.tag == "Item")
                        GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-125, 100, 50), "Pick up 'R'", guiStyle);
                    if(obj.tag == "Dealer")
                        GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-125, 100, 50), "Make a deal 'R'", guiStyle);
                    break;
                }
            }
        }
    }
}
