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
        talkData.Add(1, new string[] { "Ʃ�丮���Դϴ�.", "�����Ӱ� �ڵ����� ����� ���۹��� �����غ�����.",
            "�ͼ������ٸ� ������ �̵��Ͽ� ���� ���������� ����� �ϼ��ϼ���.", "���۹� ������ TAPŰ�Դϴ�."});
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
