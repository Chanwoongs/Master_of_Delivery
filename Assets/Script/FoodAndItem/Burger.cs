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
        // 효과음 추가
    }
    // Update is called once per frame
    void Update()
    {
        // 돌아가게
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        // 드론에 닿으면
        if (other.CompareTag("Drone"))
        {
            // 버거 소지여부 
            d.hasBurger = true;
            // 버거 끄기
            this.gameObject.SetActive(false);
        }
    }
}
