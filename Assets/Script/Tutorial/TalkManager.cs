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
        talkData.Add(1, new string[] { "튜토리얼 입니다. 앞에 보이는 포탈까지 이동하세요." });
        talkData.Add(2, new string[] { "차를 자유롭게 이용하여 포탈까지 이동하세요." });
        talkData.Add(3, new string[] { "드론을 자유롭게 연습하고 앞에 보이는 포탈까지 이동하세요." });
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
