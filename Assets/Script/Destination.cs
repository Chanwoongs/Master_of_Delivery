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
    // 플레이어 배달완료
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (p.hasCandy == false) return;
            // 사탕가게 캔디
            candy.SetActive(true);
            // 배달지 비활성화
            this.gameObject.SetActive(false);
            // 사탕 소지 false
            p.hasCandy = false;
            // 효과음 재생
            p.interactAudio.clip = p.deliverd;
            p.interactAudio.Play();
        }
    }
}
