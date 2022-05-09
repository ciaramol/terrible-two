using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    //private Rigidbody shipRigidbody;
    
    void Start()
    {
        //shipRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Crashable") return;

        Debug.Log("Trigger enter");

        //Vector3 bounceDirecton = other.gameObject.transform.position - gameObject.transform.position;
        //shipRigidbody.AddForce(bounceDirecton.normalized * 1000);
        //Debug.Log(bounceDirecton.normalized * 1000);
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag != "Crashable") return;

    //    Debug.Log("Collision enter");
    //}
}
