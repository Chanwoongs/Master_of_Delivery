using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameManager gm;
    Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnDisable()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerCar"))
        {
            if (gm.time + 10.0f > gm.timeSlider.maxValue)
            {
                gm.timeSlider.maxValue = gm.time + 10.0f;
            }
            gm.time += 10.0f;

            this.gameObject.SetActive(false);
            p.interactAudio.clip = p.deliverd;
            p.interactAudio.Play();
        }
    }
}
