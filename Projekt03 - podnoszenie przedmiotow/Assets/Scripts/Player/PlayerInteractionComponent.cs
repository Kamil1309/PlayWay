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

        foreach(GameObject obj in nearObjects)
        {
            if( obj.GetComponent<Item>().CanInteract(gameObject) )
            {
                if(obj.GetComponent<Item>().CheckCapacityLimit(gameObject)){
                    obj.GetComponent<Item>().Interact(gameObject);
                    break;
                }
            }
        }
        
        nearObjects.RemoveAll( obj => obj.activeSelf == false);
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.R)){
            Debug.Log("You tried to interact!!!");
            InteractWithInteractable();
        }
    }

    private void OnGUI() {
        foreach(GameObject obj in nearObjects){
            if( obj.GetComponent<Item>().CanInteract(gameObject) )
            {
                guiStyle.fontSize = 20;
                GUI.contentColor = Color.black;
                GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-125, 100, 50), "Press 'R'", guiStyle);
                break;
            }
        }
        
    }
}
