using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    Drone d;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 50f;
        d = GameObject.Find("Drone").GetComponent<Drone>();
    }
    private void OnDisable()
    {
        // ȿ���� �߰�
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
