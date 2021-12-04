using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBurger : MonoBehaviour
{
    Drone d;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        d = GameObject.Find("Drone").GetComponent<Drone>();
        rotSpeed = 50f;
    }
    // Update is called once per frame
    void Update()
    {
        if (d.hasBurger) transform.localScale = new Vector3(3, 3, 3);
        else if (!d.hasBurger) transform.localScale = new Vector3(0, 0, 0);

        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
