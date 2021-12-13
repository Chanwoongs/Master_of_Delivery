using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerDestination : MonoBehaviour
{
    Player p;
    Drone d;
    GameObject burger;
    GameManager gm;
    public AudioSource audioSource;  // ����� ���� �ֹ��Ҹ�=
    GameObject hambergerText;
    GameObject candyText;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        d = GameObject.Find("DroneParent").transform.GetChild(0).GetComponent<Drone>();
        burger = GameObject.Find("Burger");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        hambergerText = GameObject.Find("UICanvas").transform.GetChild(6).gameObject;
        candyText = GameObject.Find("UICanvas").transform.GetChild(7).gameObject;
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
          

            // ���� ���� false
            d.hasBurger = false;
            // ȿ���� ���
            p.interactAudio.clip = p.deliverd;
            p.interactAudio.Play();

            if (gm.isDeliverd3 != 0)
                audioSource.Play();

            // ����� ����
            if (gm.isDeliverd1 == 0)
            {
                gm.isDeliverd1 = 1;
                gm.isDeliverd2 = 0;
                candyText.SetActive(true);
            }
            else if (gm.isDeliverd2 == 0)
            {
                gm.isDeliverd2 = 1;
                gm.isDeliverd3 = 0;
                candyText.SetActive(true);
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
                hambergerText.SetActive(true);
            }
            else if (gm.isDeliverd6 == 0)
            {
                gm.isDeliverd6 = 1;
                gm.isDeliverd7 = 0;
                candyText.SetActive(true);
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
