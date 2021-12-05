using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderText : MonoBehaviour
{
    private void OnEnable()
    {
         Invoke("InvokeText", 6f);
    }

    void InvokeText()
    {
        gameObject.SetActive(false);
    }
   
}
