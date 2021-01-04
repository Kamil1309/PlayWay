using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using Interact;

public class Dealer : MonoBehaviour, IInteractable
{
    float acceptanceAngle = 40.0f;

    private void Start() {
        GetComponent<Inventory>().maxCapacity = float.MaxValue;
    }

    public bool CanInteract(GameObject player)
    {
        Vector3 playerForward = player.transform.forward;
        Vector3 dealerForward = gameObject.transform.forward;
        Vector3 dealerPlayerVec = gameObject.transform.position - player.transform.position;
        Vector3 playerDealerVec = player.transform.position - gameObject.transform.position;

        if( Vector3.Angle(playerForward, dealerPlayerVec) < acceptanceAngle && Vector3.Angle(dealerForward, playerDealerVec) < acceptanceAngle )
            return true;
        else
            return false;
    }

    public void Interact(GameObject player){ 
        GetComponent<TradeManager>().StartTrade(player);
    }
}