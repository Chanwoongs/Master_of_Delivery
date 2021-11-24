using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject sector1;
    public GameObject sector2;
    public GameObject sector3;
    public Slider timeSlider;
    public Text timeTxt;
    public Image candy1;
    public Image candy2;

    private GameObject[] desSector1;
    private GameObject[] desSector2;
    private GameObject[] desSector3;

    private int randNum1;
    private int randNum2;
    private int randNum3;
    public float time;

    // Start is called before the first frame update
    void Awake()
    {
        // 초기 시간 설정
        time = 90.0f;
        // 초기 최대 시간 설정
        timeSlider.maxValue = time;
        
        // Section 도착지들을 관리할 배열에 대입
        desSector1 = GameObject.FindGameObjectsWithTag("Section1");
        // 랜덤 수 생성
        randNum1 = Random.Range(0, desSector1.Length - 1);
        // 선택된 배달지 이외의 배달지는 꺼주기
        for (int i = 0; i < desSector1.Length; i++)
        {
            if (i == randNum1) continue;
            desSector1[i].SetActive(false);
        }

        desSector2 = GameObject.FindGameObjectsWithTag("Section2");
        randNum2 = Random.Range(0, desSector2.Length - 1);
        for (int i = 0; i < desSector2.Length; i++)
        {
            if (i == randNum2) continue;
            desSector2[i].SetActive(false);
        }

        desSector3 = GameObject.FindGameObjectsWithTag("Section3");
        randNum3 = Random.Range(0, desSector3.Length - 1);
        for (int i = 0; i < desSector3.Length; i++)
        {
            if (i == randNum3) continue;
            desSector3[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameClear();
        updateTime();
        
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
