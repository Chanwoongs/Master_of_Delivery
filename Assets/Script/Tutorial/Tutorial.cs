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
    bool first = true;
    int talkIndex = 0;

    private void Start()
    {
        TalkText = GameObject.Find("TalkText").GetComponent<Text>();
        btn = GameObject.Find("TalkButton").GetComponent<Button>();
    }

    private void Update()
    {
        if (first)
            FirstKey();

        if (isAction)
            btn.onClick.AddListener(() => Talk(1)); // 클릭시 대화가 나온다.
    }

    void FirstKey()
    {
        if (Input.anyKeyDown)
        {
            first = false;
            string talkData = null;

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
        TalkText.text = string.Empty;
    }
}
