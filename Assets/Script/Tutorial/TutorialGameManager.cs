using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialGameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public CameraMove cam;
    public Player player;
    GameObject usingMap;

    bool firstMessage = false;

    Text TalkText;
    private Button btn;

    bool isAction = true;
    bool first = true;
    bool isOpen = false;
    int talkIndex = 0;
    int num = 1;

    private void Start()
    {
        TalkText = GameObject.Find("TalkText").GetComponent<Text>();
        btn = GameObject.Find("TalkButton").GetComponent<Button>();
        usingMap = GameObject.Find("TalkCanvas").transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (first)
            FirstKey();

        if (isAction)
            btn.onClick.AddListener(() => Talk(num)); // 클릭시 대화가 나온다.


        if (Input.GetKeyDown(KeyCode.Tab) && isOpen)
        {
            if (!usingMap.activeSelf)
            {
                usingMap.SetActive(true);
            }
            else
            {
                usingMap.SetActive(false);
            }
        }
    }

    void FirstKey()
    {
        if (Input.anyKeyDown)
        {
            Pause();
            first = false;
            string talkData = talkManager.GetTalk(1, talkIndex);
            TalkText.text = string.Empty;
            StopAllCoroutines();
            StartCoroutine(Typing(talkData, 1));
        }
    }

    void Pause()
    {
        // 플레이어 이동제한
        player.SetIsMove(false);
        cam.setIsAction(false);
    }

    void Continue()
    {
        // 플레이어 이동제한 해제
        player.SetIsMove(true);
        cam.setIsAction(true);
    }

    void Talk(int id)
    {
        if (isAction == false)
            return;

        if (firstMessage)
        {
            TalkText.text = string.Empty;
            Continue();
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
    }

    public void SetIsOpen(bool open)
    {
        isOpen = open;
    }
}
