using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using QuestNS;

public class QuestGiver : MonoBehaviour
{
    protected QuestManager SubscribedPlayer;
    
    public Quest[] QuestsToGive;

    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.tag == "Player"){
            
            SubscribePlayer(other.gameObject);
        }
    }
    
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Player"){
            UnsubscribePlayer(other.gameObject);
        }
    }

    public virtual void StartQuest(){

    }

    public virtual void FinishQuest(){

    }
    
    public virtual void UpdateQuest(){

    }

    void SubscribePlayer(GameObject player){
        Debug.Log("Player subscribed by: " + gameObject.name);
        SubscribedPlayer = player.GetComponent<QuestManager>();
    }
    
    void UnsubscribePlayer(GameObject player){
        Debug.Log("Player unsubscribed by: " + gameObject.name);
        SubscribedPlayer = null;
    }

}
