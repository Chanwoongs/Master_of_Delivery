using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{ 
    // 남은 시간 UI
    public Slider timeSlider;
    // 남은 시간 Text
    public Text timeTxt;
    // 남은 시간
    public float time;

    // 남은 사탕 UI
    public Image candy1;
    public Image candy2;

    // 1미션 배달 영역
    private GameObject[] desSector1;
    private GameObject[] desSector2;
    private GameObject[] desSector3;

    // 1미션 배달 클리어 이벤트
    public UnityEvent clearedFirst;
    public UnityEvent clearedSecond;
    public UnityEvent clearedThird;

    // 배달지용 난수 생성
    private int randNum1;
    private int randNum2;
    private int randNum3;

    void Awake()
    {
        // 초기 시간 설정
        time = 90.0f;
        // 초기 최대 시간 설정
        timeSlider.maxValue = time;

        // 첫번째 영역 배달지 선정
        desSector1 = GameObject.FindGameObjectsWithTag("Section1");
        randNum1 = Random.Range(0, desSector1.Length - 1);
        // 두번째 영역 배달지 선정
        desSector2 = GameObject.FindGameObjectsWithTag("Section2");
        randNum2 = Random.Range(0, desSector2.Length - 1);
        // 세번째 영역 배달지 선정
        desSector3 = GameObject.FindGameObjectsWithTag("Section3");
        randNum3 = Random.Range(0, desSector3.Length - 1);

        Debug.Log(desSector1.Length);

        // 모든 배달지 비활성화
        setAllDestinationFalse();

        // 첫번째 배달지 활성화
        setFirstDestination();
    }

    void Update()
    {
        //gameClear();
        updateTime();
    }
    
    // 모든 배달지 비활성화
    private void setAllDestinationFalse()
    {
        for (int i = 0; i < desSector1.Length; i++)
        {
            desSector1[i].SetActive(false);
        }
        for (int i = 0; i < desSector2.Length; i++)
        {
            desSector2[i].SetActive(false);
        }
        for (int i = 0; i < desSector3.Length; i++)
        {
            desSector3[i].SetActive(false);
        }
    }

    // 첫번째 배달지 설정
    private void setFirstDestination()
    {
        desSector1[randNum1].SetActive(true);
    }

    // 두번째 배달지 설정
    private void setSecondDestination()
    {
        for (int i = 0; i < desSector2.Length; i++)
        {
            if (i == randNum2) continue;
            desSector2[i].SetActive(false);
        }
    }

    // 세번째 배달지 설정
    private void setThirdDestination()
    {
        for (int i = 0; i < desSector3.Length; i++)
        {
            if (i == randNum3) continue;
            desSector3[i].SetActive(false);
        }
    }
    private void gameClear()
    {
        int count = 0;

        for (int i = 0; i < desSector1.Length; i++)
        {
            if (desSector1[i].activeSelf == true)
            {
                count++;
            }
        }
        for (int i = 0; i < desSector2.Length; i++)
        {
            if (desSector2[i].activeSelf == true)
            {
                count++;
            }
        }
        for (int i = 0; i < desSector3.Length; i++)
        {
            if (desSector3[i].activeSelf == true)
            {
                count++;
            }
        }
        if (count == 2)
        {
            candy1.enabled = false;
        }
        else if (count == 1)
        {
            candy2.enabled = false;
        }
        else if (count == 0)
        {
            Debug.Log("GAME CLEAR");
            SceneManager.LoadScene("ClearScene");
        }
    }
    // 시간 업데이트
    private void updateTime()
    {
        // 시간 감소
        time -= Time.deltaTime;
        // 시간 슬라이더 반영
        timeSlider.value = time;
        // 시간 출력
        timeTxt.text = time.ToString("F2") + "S";
        // 시간이 다되면 게임 오버
        if (time < 0f)
        {
            Debug.Log("TIME OUT");
            SceneManager.LoadScene("FailScene");
        }
    }
}
