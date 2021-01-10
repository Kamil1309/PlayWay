using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 offset;

    [SerializeField] float velocity = 0.1f;
    [SerializeField] float angleShift = 90.0f;
    
    float deltaTime = 1/120.0f;

    private void FixedUpdate() {
        offset = Vector3.zero;

        if (Input.GetKey("up") || Input.GetKey("w")){
            offset = gameObject.transform.forward * velocity;
            UpdatePlayerPos();
        }

        if (Input.GetKey("down") || Input.GetKey("s")){
            offset = gameObject.transform.forward * -velocity;
            UpdatePlayerPos();
        }

        if (Input.GetKey("right") || Input.GetKey("d")){
            offset = gameObject.transform.right * velocity;
            UpdatePlayerPos();
        }

        if (Input.GetKey("left") || Input.GetKey("a")){
            offset = gameObject.transform.right * -velocity; 
            UpdatePlayerPos();
        }

        if (Input.GetKey(KeyCode.Q)){
            transform.Rotate(Vector3.up * -angleShift * deltaTime);
        }

        if (Input.GetKey(KeyCode.E)){
            transform.Rotate(Vector3.up * angleShift * deltaTime);
        }
    }

    private void UpdatePlayerPos(){
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + offset.x, 
                                                    gameObject.transform.position.y + offset.y, 
                                                    gameObject.transform.position.z + offset.z);
    }
}