using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCandy : MonoBehaviour
{
    Player p;
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();
        rotSpeed = 50f;
    }
    // Update is called once per frame
    void Update()
    {
        if (p.hasCandy) transform.localScale = new Vector3(1, 1, 1);
        else if(!p.hasCandy) transform.localScale = new Vector3(0, 0, 0);

        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
