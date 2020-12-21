using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private void FixedUpdate() {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z + 0.1f);
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z - 0.1f);
        }

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z);
        }

        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z);
            
        }
        if (Input.GetKey("e"))
        {
            gameObject.transform.Rotate(new Vector3(gameObject.transform.rotation.x, 
                                                    gameObject.transform.rotation.y + 2f, 
                                                    gameObject.transform.rotation.z));
        }
        if (Input.GetKey("q"))
        {
            gameObject.transform.Rotate(new Vector3(gameObject.transform.rotation.x, 
                                                    gameObject.transform.rotation.y - 2f, 
                                                    gameObject.transform.rotation.z));
        }
    }
}
