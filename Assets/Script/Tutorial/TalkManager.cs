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
        talkData.Add(1, new string[] { "Ʃ�丮���Դϴ�.", "�����Ӱ� �ڵ����� ����� ��������.", "�ͼ������ٸ� ������ �̵��Ͽ� ����� �ϼ��ϼ���."});
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
