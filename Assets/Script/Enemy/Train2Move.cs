using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Train2Move : MonoBehaviour
{
    Player p;
    CarController cc;
    Vector3 currentPos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            p = GameObject.Find("Player").GetComponent<Player>();
            cc = GameObject.Find("DeliveryCar").GetComponent<CarController>();
        }

        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        resetTrain();
    }

    void resetTrain()
    {
        if (this.currentPos.z - this.transform.position.z > 100) 
        {
            transform.position = currentPos;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            p.interactAudio.clip = p.boom;
            p.interactAudio.Play();
            Debug.Log("TRAIN CRUSH");
            SceneManager.LoadScene("FailScene");
        }
        else if (collision.collider.CompareTag("PlayerCar"))
        {
            if (!cc.isDriving) return;
            p.interactAudio.clip = p.boom;
            p.interactAudio.Play();
            Debug.Log("TRAIN CRUSH");
            SceneManager.LoadScene("FailScene");
        }
    }
}
