using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public TalkManager talkManager;
    public CameraMove cam;
    Text TalkText;
    private Button btn;

    bool isAction;
    bool first;
    int talkIndex;
    int num;

    private void Start()
    {
        TalkText = GameObject.Find("TalkText").GetComponent<Text>();
        btn = GameObject.Find("TalkButton").GetComponent<Button>();
        num = 1;
        talkIndex = 0;
        first = true;
        isAction = true;

    }

    private void Update()
    {
        //if (first)
        //    FirstKey();

        btn.onClick.AddListener(() => Talk(num)); // 클릭시 대화가 나온다.
    }

    void FirstKey()
    {
        if(Input.anyKeyDown)
        {
            Pause();
            first = false;
            Talk(num);
        }
    }

    void Pause()
    {
        // 플레이어 이동제한
        cam.setIsAction(false);
        
    }

    void Continue()
    {
        // 플레이어 이동제한 해제
        cam.setIsAction(true);
    }

    void Talk(int id)
    {
        if (id > talkManager.getTalkDataSize())
        {
            TalkText.text = string.Empty;
            return;
        }

        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            num += 1;
            return;
        }

        TalkText.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(typing(talkData, id));
        isAction = true;

    }

    IEnumerator typing(string data, int id)
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
        yield return new WaitForSeconds(0.1f);
    }
}
