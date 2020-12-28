using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionComponent : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> nearEnemies = new List<GameObject>();
}
