using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGun : MonoBehaviour
{
    public Transform gunTransform;
    public GameObject bulletPrefab;
    public float fireRate = 6;
    private float waitTillNextFire = 0.0f;
    private Transform target;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Drone").transform;
    }

    void Update()
    {

    }

    private void ShootingBullets()
    {
       if (waitTillNextFire <= 0)
       {
            Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
            waitTillNextFire = 5;
            Debug.Log("Fire");
       }
       
       waitTillNextFire -= fireRate * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Drone"))
        {
            transform.LookAt(target);
            ShootingBullets();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
