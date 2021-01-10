using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            InitBullet();
        }
    }

    private void InitBullet(){
        Vector3 PlayerPos = gameObject.transform.position;

        GameObject createdBullet= Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        createdBullet.transform.position = new Vector3(PlayerPos.x, PlayerPos.y, PlayerPos.z) + gameObject.transform.forward * 0.8f;
        createdBullet.transform.forward = gameObject.transform.forward;
    }
}
