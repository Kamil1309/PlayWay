using UnityEngine;

namespace QuestNS{
    public class Quest{
        public float _reward;
        public string _name;
        public int _ID;
        public string _description;

        public Quest(float reward, string name, int ID, string description){
            _reward = reward;
            _name = name;
            _ID = ID;
            _description = description;
        }

        public void OnFinished(){
            Debug.Log("Quest " + _name + " finished.");
        }
        public void OnStart(){
            Debug.Log("Quest " + _name + " started.");
        }
        public void OnUpdate(){
            Debug.Log("Quest " + _name + " updated.");
        }

    }
}