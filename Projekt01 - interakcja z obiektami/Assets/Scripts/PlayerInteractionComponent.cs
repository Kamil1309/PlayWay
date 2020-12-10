using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inter;

public class PlayerInteractionComponent : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> colidedObjects = new List<GameObject>();

    public void InteractWithInteractable(){
        foreach(GameObject obj in colidedObjects)
        {
            if( obj.GetComponent<Interactable>().CanInteract(gameObject) )
            {
                obj.GetComponent<Interactable>().Interact();
            }
        }
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.E)){
            Debug.Log("You tried to interact!!!");
            InteractWithInteractable();
        }
    }
}
