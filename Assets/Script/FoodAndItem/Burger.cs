using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    Drone d;
    Player p;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 50f;
        d = GameObject.Find("DroneParent").transform.GetChild(0).GetComponent<Drone>();
        p = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnDisable()
    {
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
            // ���� �������� 
            d.hasBurger = true;
            // ���� ����
            this.gameObject.SetActive(false);
        }
    }
}
