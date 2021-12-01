using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingRotate : MonoBehaviour
{
    private float rotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 5000;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
    }
}
