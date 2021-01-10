using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceManager : MonoBehaviour
{
    [SerializeField] float velocity = 2.0f;

    Rigidbody rb;
    private Vector3 oldVelocity;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody>();

        rb.velocity = transform.forward * velocity;
        rb.freezeRotation = true;
    }

    void FixedUpdate () {
        oldVelocity = rb.velocity;
    }

    void OnCollisionEnter (Collision collision) {
        ContactPoint contact = collision.contacts[0];
        
        Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);        
        
        rb.velocity = reflectedVelocity;
        
        Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
        transform.rotation = rotation * transform.rotation;
    }
}
