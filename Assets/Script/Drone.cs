using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject candy;
    public GameObject burger;

    public bool hasCandy;
    public bool hasBurger;

    private void Start()
    {
        hasCandy = false;
        hasBurger = false;
    }
}
