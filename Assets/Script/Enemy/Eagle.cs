using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField] private int direction;
    private float timer;
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        direction = 1;  
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        moveUpDown();
        moveForward();
    }

    private void moveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 3f);
    }
    private void moveUpDown()
    {
        if (direction == 1)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        else if (direction == -1)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        changeDirection();
    }

    private void changeDirection()
    {
        timer += Time.deltaTime;

        if (timer > 5.0f)
        {
            if (direction == 1)
            {
                direction = -1;
            }
            else if (direction == -1)
            {
                direction = 1;
            }
            timer = 0;
        }
    }
}
