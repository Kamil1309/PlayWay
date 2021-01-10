using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offset;

    float lastAngle;

    void Start () {
        offset = new Vector3(player.position.x, player.position.y + 1.5f, player.position.z - 2.4f);
        lastAngle = player.transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(-(lastAngle - player.transform.eulerAngles.y), Vector3.up) * offset;
        transform.position = player.position + offset; 
        transform.LookAt(new Vector3(player.position.x, player.position.y + 1.0f, player.position.z));

        lastAngle = player.transform.eulerAngles.y;
    }
} 