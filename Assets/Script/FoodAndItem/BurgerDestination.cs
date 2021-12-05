using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerDestination : MonoBehaviour
{
    Player p;
    Drone d;
    GameObject burger;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        d = GameObject.Find("DroneParent").transform.GetChild(0).GetComponent<Drone>();
        burger = GameObject.Find("Burger");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // �÷��̾� ��޿Ϸ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drone"))
        {
            if (d.hasBurger == false) return;
            // ������ ����
            burger.SetActive(true);

            // ���� ���� false
            d.hasBurger = false;
            // ȿ���� ���
            p.interactAudio.clip = p.deliverd;
            p.interactAudio.Play();

            // ����� ����
            if (gm.isDeliverd1 == 0)
            {
                gm.isDeliverd1 = 1;
                gm.isDeliverd2 = 0;
            }
            else if (gm.isDeliverd2 == 0)
            {
                gm.isDeliverd2 = 1;
                gm.isDeliverd3 = 0;
            }
            else if (gm.isDeliverd3 == 0)
            {
                gm.isDeliverd3 = 1;
                gm.isDeliverd4 = 0;
            }
            else if (gm.isDeliverd4 == 0)
            {
                gm.isDeliverd4 = 1;
                gm.isDeliverd5 = 0;
            }
            else if (gm.isDeliverd5 == 0)
            {
                gm.isDeliverd5 = 1;
                gm.isDeliverd6 = 0;
            }
            else if (gm.isDeliverd6 == 0)
            {
                gm.isDeliverd6 = 1;
                gm.isDeliverd7 = 0;
            }
            else if (gm.isDeliverd7 == 0)
            {
                gm.isDeliverd7 = 1;
            }


            // ����� ��Ȱ��ȭ
            this.gameObject.SetActive(false);
        }
    }
}
