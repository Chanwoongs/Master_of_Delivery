using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 90f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 20;
    public Vector3 centerOfMass;
    Player p;
    private List<Transform> nodes;
    private int currentNode = 0;
    // Start is called before the first frame update
    private void Start()
    {      

        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        p = GameObject.Find("Player").GetComponent<Player>();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWayPointDistance();
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }
    private void Drive()
    {
        wheelFL.motorTorque = maxMotorTorque;
        wheelFR.motorTorque = maxMotorTorque;
    }
    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.05f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("BUSTED");
            SceneManager.LoadScene("FailScene");
        }
        else if (collision.collider.CompareTag("PlayerCar"))
        {
            p.interactAudio.clip = p.boom;
            p.interactAudio.Play();
            Debug.Log("BUSTED");
            SceneManager.LoadScene("FailScene");
        }
    }
}
