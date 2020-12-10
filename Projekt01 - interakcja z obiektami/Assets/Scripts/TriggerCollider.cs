using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
            FindObjectOfType<PlayerInteractionComponent>().colidedObjects.Add(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "player")
            FindObjectOfType<PlayerInteractionComponent>().colidedObjects.Remove(gameObject);
    }
}
