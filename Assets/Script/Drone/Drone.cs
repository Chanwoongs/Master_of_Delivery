using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject candy;
    public GameObject burger;

    public GameObject droneCandy;
    public GameObject droneBurger;

    public GameObject playerCamera;
    Player p;
    public GameObject pickableDrone;
    PickableDrone pd;

    public float height;

    GameManager gm;

    public bool hasCandy;
    public bool hasBurger;

    public int hp = 100;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        hasCandy = false;
        hasBurger = false;
        pd = pickableDrone.GetComponent<PickableDrone>();
        p = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (transform.localPosition.y > 20)
        {
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
        }
        if (transform.localPosition.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        height = transform.position.y;
        OnDroneDestroyed();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            hp -= 20;
        }
    }

    private void OnDroneDestroyed()
    {
        if (hp <= 0)
        {
            pd.isControlling = false;
            GameObject.Find("DroneCamera").SetActive(false);
            playerCamera.SetActive(true);
            gameObject.SetActive(false);
            gm.droneUI.gameObject.SetActive(false);
            hp = 100;
            p.GetComponent<Animator>().enabled = true;
            if (hasBurger)
            {
                hasBurger = false;
                droneBurger.transform.localScale = new Vector3(0, 0, 0);
                burger.SetActive(true);
            }
            if (hasCandy)
            {
                hasCandy = false;
                droneCandy.transform.localScale = new Vector3(0, 0, 0);
                candy.SetActive(true);
            }
        }
    }
}