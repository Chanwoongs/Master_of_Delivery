using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    Animator animator;

    [SerializeField] bool detected;

    public Transform path;
    private List<Transform> nodes;
    private int currentNode = 0;

    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
        detected = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        moveAround();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("CAUGHT BY HUNGRY MOSTER");
            SceneManager.LoadScene("FailScene");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
            agent.destination = target.transform.position; // 쫓아갈 위치 설정
            agent.speed = 1.5f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        agent.destination = transform.position;
        detected = false;
    }
    private void moveAround()
    {
        if (!detected)
        {

            if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.5f)
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

            agent.destination = nodes[currentNode].position;
        }
    }
}
