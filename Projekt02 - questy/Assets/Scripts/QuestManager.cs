using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestNS;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public List<Quest> Quests = new List<Quest>(); 

    // public GameObject questPanel;
    // public Text questsText;
    
    public void OnGUI(){
        //Debug.Log(Quests[0]._name + "  " + Quests[0]._name + "  " + Quests[0]._name);
        //Debug.Log(Quests.Count);
        string newText = "";

        foreach(Quest quest in Quests){
            newText += quest._name + "\n" + quest._description + "\n";
        }
        
        GUI.Box(new Rect(0, 0, 400, 200), newText);

        //this.enabled = false;
    }
    

}
