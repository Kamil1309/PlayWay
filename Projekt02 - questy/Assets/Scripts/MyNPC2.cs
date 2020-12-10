using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestNS;
using System;

public class MyNPC2 : QuestGiver
{
    bool passed = false;

    private void Start() {

        Quest quest1 = new Quest(1000, "Quest 1", 1, "This is your first Quest, go to green npc and click 'space' gl");

        QuestsToGive = new Quest[1];
        QuestsToGive[0] = quest1;
    }

    public override void FinishQuest(){
        if( SubscribedPlayer.Quests.Exists( q => q._ID == QuestsToGive[0]._ID) ){
            Quest qToRemove = SubscribedPlayer.Quests.Find( q => q._ID == QuestsToGive[0]._ID);
            SubscribedPlayer.Quests.Remove( qToRemove );
        }
    }

    private void Update() {
        if(SubscribedPlayer != null && passed == false){
            if(Input.GetKey("space")){
                passed = true;
                FinishQuest();
            }
        }
    }
}