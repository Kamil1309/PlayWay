using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private void FixedUpdate() {
        if (Input.GetKey("up"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z + 0.1f);
        }

        if (Input.GetKey("down"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z - 0.1f);
        }

        if (Input.GetKey("right"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z);
        }

        if (Input.GetKey("left"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.1f, 
                                                        gameObject.transform.position.y, 
                                                        gameObject.transform.position.z);
            
        }
    }
}
