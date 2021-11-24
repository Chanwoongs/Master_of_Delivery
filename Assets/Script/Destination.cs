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
    private void OnDisable()
    {
        p.interactAudio.clip = p.deliverd;
        p.interactAudio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (p.hasCandy == false) return;
            candy.SetActive(true);
            this.gameObject.SetActive(false);
            p.hasCandy = false;
        }
    }
}
