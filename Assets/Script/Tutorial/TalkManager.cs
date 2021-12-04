using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    TutorialGameManager tutorial;

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        tutorial = GameObject.FindObjectOfType<TutorialGameManager>().GetComponent<TutorialGameManager>();
        AddData();
    }

    void AddData()
    {
        talkData.Add(1, new string[] { "튜토리얼입니다.", "자유롭게 자동차와 드론의 조작법을 연습해보세요.",
            "익숙해졌다면 집으로 이동하여 다음 스테이지의 배달을 완수하세요.", "조작법 설명은 TAP키입니다."});
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            tutorial.SetIsOpen(true);
            return null;
        }
            
        else
            return talkData[id][talkIndex];
    }

    public int getTalkDataSize()
    {
        return talkData.Count;
    }
}
