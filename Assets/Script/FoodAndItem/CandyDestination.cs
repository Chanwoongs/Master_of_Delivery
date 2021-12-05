using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyDestination : MonoBehaviour
{
    Player p;
    Drone d;
    GameObject candy;
    GameManager gm;
    public AudioSource audioSource;  // 배달의 민족 주문소리
    GameObject candyText;
    GameObject hambergerText;
    

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        d = GameObject.Find("DroneParent").transform.GetChild(0).GetComponent<Drone>();
        candy = GameObject.Find("Candy");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        candyText = GameObject.Find("UICanvas").transform.GetChild(7).gameObject;
        hambergerText = GameObject.Find("UICanvas").transform.GetChild(6).gameObject;

        candyText.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (p.hasCandy == false) return;
           
            p.hasCandy = false;

            p.interactAudio.clip = p.deliverd;
            p.interactAudio.Play();

            if(gm.isDeliverd4 != 0)
                audioSource.Play();

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
                candyText.SetActive(true);
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

            this.gameObject.SetActive(false);
        }

        if (other.CompareTag("Drone"))
        {
            if (d.hasCandy == false) return;

            candy.SetActive(true);
            
            d.hasCandy = false;

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
                candyText.SetActive(true);
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

            this.gameObject.SetActive(false);
        }
    }

   
}
