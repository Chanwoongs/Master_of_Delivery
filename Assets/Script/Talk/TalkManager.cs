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
        talkData.Add(1, new string[] { "어서오세요.", "배달마스터 튜토리얼입니다.", "이제부터 조작법를 배워봅시다." , "WASD를 이용하여 플레이어를 움직여보세요."});
        talkData.Add(2, new string[] { "잘하셨습니다.", "다음은 자동차를 운전해봅시다.", "자동차 근처로가서 F키를 눌러 차를 타세요" });
        talkData.Add(3, new string[] { "플레이어와 마찬가지로 WASD를 이용하여 움직여 봅시다.", "자동차의 급정지는 Space바입니다." });
        talkData.Add(4, new string[] { "잘하셨습니다.", "다음은 드론을 조종해봅시다.", "드론 근처로가서 F키를 눌러 드론을 조종하세요" });
        talkData.Add(5, new string[] { "Q와E를 활용하여 드론을 움직여 봅시다." });
        talkData.Add(6, new string[] { "잘하셨습니다.", "이제부터 자유롭게 자동차와 드론을 익히고 익숙해졌으면 옆 포탈로 가세요."});
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
