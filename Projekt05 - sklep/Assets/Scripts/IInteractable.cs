using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Interact{

    interface IInteractable
    {
        bool CanInteract(GameObject player);
        void Interact(GameObject player);
    }
    
}