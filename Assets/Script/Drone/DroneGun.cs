using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneGun : MonoBehaviour
{
    public Transform gunTransform;
    public GameObject bulletPrefab;
    public AudioSource policeAudio;

    public float fireRate = 6;
    private float waitTillNextFire = 0.0f;
    private Transform target;
    private void Awake()
    {
        target = GameObject.Find("DroneParent").transform.GetChild(0).transform;
    }

    private void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drone"))
        {
            policeAudio.Play();
        }
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
        if (other.CompareTag("Drone"))
        {
            policeAudio.Play();
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
