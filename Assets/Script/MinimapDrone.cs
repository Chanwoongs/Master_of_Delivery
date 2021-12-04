using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDrone : MonoBehaviour
{
    public GameObject drone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(drone.transform.position.x, drone.transform.position.y + 20, drone.transform.position.z);
    }
}
