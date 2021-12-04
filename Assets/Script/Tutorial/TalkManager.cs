using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        AddData();
    }

    void AddData()
    {
        talkData.Add(1, new string[] { "튜토리얼입니다.", "자유롭게 자동차와 드론을 익히세요.", "익숙해졌다면 집으로 이동하여 배달을 완수하세요."});
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public int getTalkDataSize()
    {
        return talkData.Count;
    }
}
