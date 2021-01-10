using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 offset;

    private void FixedUpdate() {
        offset = Vector3.zero;

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            offset = gameObject.transform.forward * 0.1f;
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            offset = gameObject.transform.forward * -0.1f;
        }

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            offset = gameObject.transform.right * 0.1f;
        }

        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            offset = gameObject.transform.right * -0.1f; 
        }
        
        if(offset.magnitude != 0)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + offset.x, 
                                                        gameObject.transform.position.y + offset.y, 
                                                        gameObject.transform.position.z + offset.z);

        if (Input.GetKey(KeyCode.Q)){
            transform.Rotate(-Vector3.up * 45.0f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E)){
            transform.Rotate(Vector3.up * 45.0f * Time.deltaTime);
        }
    }
}