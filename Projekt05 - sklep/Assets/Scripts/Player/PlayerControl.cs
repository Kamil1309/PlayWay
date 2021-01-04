using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 offset;

    [SerializeField] float moveDist = 0.1f;
    [SerializeField] float angleShift = 90.0f;

    bool isInventoryOpen = false;

    private void FixedUpdate() {
        if(!GetComponent<Inventory>().isDuringTrade){
            offset = Vector3.zero;

            if (Input.GetKey("up") || Input.GetKey("w")){
                offset = gameObject.transform.forward * moveDist;
                UpdatePlayerPos();
            }

            if (Input.GetKey("down") || Input.GetKey("s")){
                offset = gameObject.transform.forward * -moveDist;
                UpdatePlayerPos();
            }

            if (Input.GetKey("right") || Input.GetKey("d")){
                offset = gameObject.transform.right * moveDist;
                UpdatePlayerPos();
            }

            if (Input.GetKey("left") || Input.GetKey("a")){
                offset = gameObject.transform.right * -moveDist; 
                UpdatePlayerPos();
            }

            if (Input.GetKey(KeyCode.Q)){
                transform.Rotate(-Vector3.up * angleShift * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E)){
                transform.Rotate(Vector3.up * angleShift * Time.deltaTime);
            }
        }
    }

    private void Update() {
        if(!GetComponent<Inventory>().isDuringTrade){
            if(Input.GetKeyDown(KeyCode.Tab)){
                if(isInventoryOpen){
                    GetComponent<Inventory>().CloseInventory();
                    isInventoryOpen = false;
                }
                else{
                    GetComponent<Inventory>().ShowInventory(new Vector2(10.0f, 20.0f));
                    isInventoryOpen = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.R)){
                GetComponent<PlayerInteractionComponent>().InteractWithInteractable();
            }
        }
    }

    private void UpdatePlayerPos(){
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + offset.x, 
                                                    gameObject.transform.position.y + offset.y, 
                                                    gameObject.transform.position.z + offset.z);
    }
}