using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletForce = 100.0f;

    private void Awake()
    {
       
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletForce * Time.deltaTime);
        Destroy(gameObject, 10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Drone"))
        {
            Destroy(gameObject);
            Debug.Log("hit");
        }
    }
}