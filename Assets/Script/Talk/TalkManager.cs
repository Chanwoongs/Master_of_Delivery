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
        talkData.Add(1, new string[] { "�������.", "��޸����� Ʃ�丮���Դϴ�.", "�������� ���۹��� ������ô�." , "WASD�� �̿��Ͽ� �÷��̾ ������������."});
        talkData.Add(2, new string[] { "���ϼ̽��ϴ�.", "������ �ڵ����� �����غ��ô�.", "�ڵ��� ��ó�ΰ��� FŰ�� ���� ���� Ÿ����" });
        talkData.Add(3, new string[] { "�÷��̾�� ���������� WASD�� �̿��Ͽ� ������ ���ô�.", "�ڵ����� �������� Space���Դϴ�." });
        talkData.Add(4, new string[] { "���ϼ̽��ϴ�.", "������ ����� �����غ��ô�.", "��� ��ó�ΰ��� FŰ�� ���� ����� �����ϼ���" });
        talkData.Add(5, new string[] { "Q��E�� Ȱ���Ͽ� ����� ������ ���ô�." });
        talkData.Add(6, new string[] { "���ϼ̽��ϴ�.", "�������� �����Ӱ� �ڵ����� ����� ������ �ͼ��������� �� ��Ż�� ������."});
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
