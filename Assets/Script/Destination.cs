using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    Player p;
    GameObject candy;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        candy = GameObject.Find("Candy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // �÷��̾� ��޿Ϸ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (p.hasCandy == false) return;
            // �������� ĵ��
            candy.SetActive(true);
            // ����� ��Ȱ��ȭ
            this.gameObject.SetActive(false);
            // ���� ���� false
            p.hasCandy = false;
            // ȿ���� ���
            p.interactAudio.clip = p.deliverd;
            p.interactAudio.Play();
        }
    }
}
