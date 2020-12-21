using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborhoodTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            other.gameObject.GetComponent<PlayerInteractionComponent>().nearObjects.Add(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            other.gameObject.GetComponent<PlayerInteractionComponent>().nearObjects.Remove(gameObject);
        }
    }
}
