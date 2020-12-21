using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inter{
    public class Interactable : MonoBehaviour
    {
        public virtual bool CanInteract(GameObject obj){
            return false;
        }

        public virtual void Interact(){}
    }
}
