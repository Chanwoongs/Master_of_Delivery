using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public TalkManager talkManager;
    public CameraMove cam;

    public SimpleSampleCharacterControl player;

    bool firstMessage = false;

    Text TalkText;
    private Button btn;

    bool isAction = true;
    bool first;
    int talkIndex = 0;
    string talkData;

    public GameObject pause;
    bool textEnd;
    private void Start()
    {
        talkData = null;
        first = true;
        textEnd = false;
        TalkText = GameObject.Find("TalkText").GetComponent<Text>();
        btn = GameObject.Find("TalkButton").GetComponent<Button>();
        pause = GameObject.Find("TalkCanvas").transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        string talkData = null;

        if (first)
            FirstKey();

        if(Input.GetKeyDown(KeyCode.Escape) && textEnd)
        {
            if (!pause.activeSelf)
            {
                if (SceneManager.GetActiveScene().name == "PlayerTutorial")
                {
                    cam.setIsAction(false);
                    Time.timeScale = 0;
                }
                   
                else if (SceneManager.GetActiveScene().name == "CarTutorial" || SceneManager.GetActiveScene().name == "DroneTutorial")
                    Time.timeScale = 0;
         
                pause.SetActive(true);
            }
            else if (pause.activeSelf)
            {
                if (SceneManager.GetActiveScene().name == "PlayerTutorial")
                {
                    cam.setIsAction(true);
                    Time.timeScale = 1;
                }
                else if (SceneManager.GetActiveScene().name == "CarTutorial" || SceneManager.GetActiveScene().name == "DroneTutorial")
                    Time.timeScale = 1;
              
                pause.SetActive(false);
            }
        }

    }

    void FirstKey()
    {
        if (Input.anyKeyDown)
        {
            first = false;
           

            if (SceneManager.GetActiveScene().name == "PlayerTutorial")
            {
                talkData = talkManager.GetTalk(1, talkIndex);
            }
            else if (SceneManager.GetActiveScene().name == "CarTutorial")
            {
                talkData = talkManager.GetTalk(2, talkIndex);
            }
            else if (SceneManager.GetActiveScene().name == "DroneTutorial")
            {
                talkData = talkManager.GetTalk(3, talkIndex);
            }
            
            TalkText.text = string.Empty;
            StopAllCoroutines();
            StartCoroutine(Typing(talkData, 1));
        }
    }
    
    void Talk(int id)
    {
        if (isAction == false)
            return;

        if (firstMessage)
        {
            TalkText.text = string.Empty;
        }

        isAction = false;

        if (id > talkManager.getTalkDataSize())
        {
            TalkText.text = string.Empty;
            return;
        }

        string talkData = talkManager.GetTalk(id, talkIndex);

       
        if (talkData == null)
        {
            isAction = true;
            firstMessage = true;
            return;
        }

        TalkText.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(Typing(talkData, id));
    }

    IEnumerator Typing(string data, int id)
    {
        int i = 0;
        for (i = 0; i <= data.Length; i++)
        {
            if(i == data.Length)
            {
                talkIndex++;
            }
            TalkText.text = data.Substring(0, i);

            yield return new WaitForSeconds(0.15f);
        }
        isAction = true;
        Invoke("EmptyText", 2f);
    }

    void EmptyText()
    {
        textEnd = true;
        TalkText.text = string.Empty;
    }
}
