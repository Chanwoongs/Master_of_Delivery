using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Candy : MonoBehaviour
{
    Player p;
    Drone d;
    GameManager gm;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 50f;
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            p = GameObject.Find("Player").GetComponent<Player>();
            d = GameObject.Find("DroneParent").transform.GetChild(0).GetComponent<Drone>();
        }
        
    }
    private void OnDisable()
    {
        if (p == null) return;
        p.interactAudio.clip = p.getCandy;
        p.interactAudio.volume = 1.0f;
        p.interactAudio.Play();
        p.interactAudio.volume = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        // ���� ���ư���
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾� ������
        if (other.CompareTag("Player"))
        {
            if (gm.isDeliverd4 != -1) return;
            // ���� �������� 
            p.hasCandy = true;
        }
        // ��� ������
        if (other.CompareTag("Drone"))
        {
            if (gm.isDeliverd4 == 0 || gm.isDeliverd6 == 0) return;
            Debug.Log("Drone got candy");
            // ���� �������� 
            d.hasCandy = true;
        }
    }
}