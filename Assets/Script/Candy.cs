using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    Player p;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 50f;
        p = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnDisable()
    {
        p.interactAudio.clip = p.getCandy;
        p.interactAudio.volume = 1.0f;
        p.interactAudio.Play();
        p.interactAudio.volume = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        // ªÁ≈¡ µπæ∆∞°∞‘
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        // «√∑π¿ÃæÓ ¥Í¿∏∏È
        if (other.CompareTag("Player"))
        {
            // ªÁ≈¡ º“¡ˆø©∫Œ 
            p.hasCandy = true;
            // ªÁ≈¡ ≤Ù±‚
            this.gameObject.SetActive(false);
        }
    }
}
