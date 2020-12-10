using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestNS;
using System;

public class MyNPC : QuestGiver
{
    bool passed = false;

    private void Start() {

        Quest quest1 = new Quest(1000, "Quest 1", 1, "This is your first Quest, go to green npc and click 'space' gl");
        Quest quest2 = new Quest(500, "Quest 2", 2, "This is your second Quest, gl");
        Quest quest3 = new Quest(200, "Quest 3", 3, "This is your third Quest, gl");

        QuestsToGive = new Quest[3];
        QuestsToGive[0] = quest1;
        QuestsToGive[1] = quest2;
        QuestsToGive[2] = quest3;
    }

    public override void StartQuest(){
        foreach(Quest quest in QuestsToGive){
            if( !SubscribedPlayer.Quests.Exists( q => q._ID == quest._ID) )
                SubscribedPlayer.Quests.Add(quest);
        }
    }

    private void Update() {
        if(SubscribedPlayer != null && passed == false){
            if(Input.GetKey("space")){
                passed = true;
                StartQuest();
            }
        }
    }
}