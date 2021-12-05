using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTutorialCamera : MonoBehaviour
{
    private Transform Drone;

    private Vector3 velocityCameraFollow2;
    public Vector3 behindPosition1 = new Vector3(0, 2, -4);
    public float angle3;

    private void Awake()
    {
        Drone = GameObject.Find("Drone").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Drone.transform.TransformPoint(behindPosition1) + Vector3.up * Input.GetAxis("Vertical") * 0.05f, ref velocityCameraFollow2, 0.01f);
        transform.rotation = Quaternion.Euler(new Vector3(angle3, Drone.GetComponent<DroneTutorial>().currentYRotation, 0));
    }
}