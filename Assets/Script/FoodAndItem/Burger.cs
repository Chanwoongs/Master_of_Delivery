using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    Drone d;
    Player p;
    GameManager gm;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 50f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        d = GameObject.Find("DroneParent").transform.GetChild(0).GetComponent<Drone>();
        p = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnDisable()
    {
        if (p == null) return;
        // ȿ���� �߰�
        p.interactAudio.clip = p.getCandy;
        p.interactAudio.volume = 1.0f;
        p.interactAudio.Play();
        p.interactAudio.volume = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        // ���ư���
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        // ��п� ������
        if (other.CompareTag("Drone"))
        {
            if (gm.isDeliverd5 == 0 || gm.isDeliverd7 == 0) return;
            // ���� �������� 
            d.hasBurger = true;
        }
    }
}
