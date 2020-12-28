using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using combat;

public class NeighborhoodTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            other.gameObject.GetComponent<PlayerInteractionComponent>().nearEnemies.Add(gameObject);
            gameObject.GetComponent<CombatSystemAI>().player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            other.gameObject.GetComponent<PlayerInteractionComponent>().nearEnemies.Remove(gameObject);
            gameObject.GetComponent<CombatSystemAI>().player = null;
        }
    }
}
