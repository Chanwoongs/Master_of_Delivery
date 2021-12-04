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

    // 남은 Oil UI
    public Slider oilSlider;
    // 남은 오일 Text
    public Text oilTxt;
    // 현재 남은 오일 상태
    public float remainingOil;
    int currentOil;
    bool isInCar = false;

    // 남은 사탕 UI
    public Image candy1;
    public Image candy2;

    // 배달 완료 Flags 
    // -1 -> 초기상태, 0 -> 배달 X, 1 -> 배달 O
    [SerializeField] public int isDeliverd1;
    [SerializeField] public int isDeliverd2;
    [SerializeField] public int isDeliverd3;
    [SerializeField] public int isDeliverd4;
    [SerializeField] public int isDeliverd5;
    [SerializeField] public int isDeliverd6;
    [SerializeField] public int isDeliverd7;
    
    // 배달지용 난수 생성
    private int randNum1;
    private int randNum2;
    private int randNum3;
    private int randNum4;
    private int randNum5;
    private int randNum6;
    private int randNum7;

    public GameObject playerCamera;
    public GameObject secondLand;
    public GameObject secondLandCamera;
    Vector3 target;
    public UnityEvent toSecondLand;

    CarController cc;

    private void Start()
    {
        isDeliverd1 = 0;
        isDeliverd2 = -1;
        isDeliverd3 = -1;
        isDeliverd4 = -1;
        isDeliverd5 = -1;
        isDeliverd6 = -1;
        isDeliverd7 = -1;

        // 초기 시간 설정
        time = 18000.0f;
        // 초기 최대 시간 설정
        timeSlider.maxValue = time;
        // 초기 오일 설정
        remainingOil = 100f;
        // 최대 오일 게이지 설정
        oilSlider.maxValue = remainingOil;

        // 배달지 난수 생성
        randNum1 = Random.Range(0, 3);
        randNum2 = Random.Range(0, 39);
        randNum3 = Random.Range(0, 19);
        randNum4 = Random.Range(0, 12);
        randNum5 = Random.Range(0, 19);
        randNum6 = Random.Range(0, 10);
        randNum7 = Random.Range(0, 11);

        secondLand.transform.position = new Vector3(secondLand.transform.position.x, -200f, secondLand.transform.position.z);
        target = new Vector3(secondLand.transform.position.x, 0f, secondLand.transform.position.z);

        // 첫번째 영역 배달지 활성화
        GameObject.Find("Section1").transform.GetChild(randNum1).gameObject.SetActive(true);
        cc = GameObject.Find("DeliveryCar").GetComponent<CarController>();
    }

    void Update()
    {
        gameClear();
        updateTime();
        updateDestination();

        if(isInCar)
        {
            UpdateOilgaGage();
        }
    }

    // 배달지 업데이트
    private void updateDestination()
    {
        if (isDeliverd2 == 0)
        {
            activate2ndDestination();
        }
        if (isDeliverd3 == 0)
        {
            activate3rdDestination();
        }
        if (isDeliverd4 == 0)
        {
            secondLand.transform.position = Vector3.MoveTowards(secondLand.transform.position, target, 0.1f);
            if (toSecondLand != null)
            {
                toSecondLand.Invoke();
            }
            toSecondLand = null;
            activate4thDestination();
        }
        if (isDeliverd5 == 0)
        {
            activate5thDestination();
        }
        if (isDeliverd6 == 0)
        {
            activate6thDestination();
        }
        if (isDeliverd7 == 0)
        {
            activate7thDestination();
        }
    }

    // 두번째 배달지 활성화
    private void activate2ndDestination()
    {
        GameObject.Find("Section2").transform.GetChild(randNum2).gameObject.SetActive(true);
    }
    // 세번째 배달지 설정
    private void activate3rdDestination()
    {
        GameObject.Find("Section3").transform.GetChild(randNum3).gameObject.SetActive(true);
    }
    // 네번째 배달지 설정 (햄버거 배달지)
    private void activate4thDestination()
    {
        GameObject.Find("Section4").transform.GetChild(randNum4).gameObject.SetActive(true);
    }
    // 다섯번째 배달지 설정 (햄버거 배달지)
    private void activate5thDestination()
    {
        GameObject.Find("Section5").transform.GetChild(randNum5).gameObject.SetActive(true);
    }
    // 세번째 배달지 설정 (사탕 배달지)
    private void activate6thDestination()
    {
        GameObject.Find("Section6").transform.GetChild(randNum6).gameObject.SetActive(true);
    }
    // 세번째 배달지 설정 (사탕 배달지)
    private void activate7thDestination()
    {
        GameObject.Find("Section7").transform.GetChild(randNum7).gameObject.SetActive(true);
    }

    private void gameClear()
    {
        if (isDeliverd7 == 1)
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
        timeTxt.text = time.ToString("F1") + "s";
        // 시간이 다되면 게임 오버
        if (time < 0f)
        {
            Debug.Log("TIME OUT");
            SceneManager.LoadScene("FailScene");
        }
    }

    public void UpdateOilgaGage()
    {
        remainingOil -= Time.deltaTime;
        oilSlider.value = remainingOil;
        currentOil = (int)remainingOil;
        oilTxt.text = currentOil.ToString() + "%";

        if (remainingOil < 0f)
        {
            remainingOil = 0; 
            cc.setMotorForce(0);  // 자동차의 힘을 0으로
            cc.rb.velocity *= 0.1f;
        }
    }

    public void SetIsInCar(bool inCar)
    {
        isInCar = inCar;
    }

    public bool GetIsCar()
    {
        return isInCar;
    }

    public void changeCameraToSecondLand()
    {
        secondLand.transform.position = new Vector3(secondLand.transform.position.x, -40f, secondLand.transform.position.z);
        playerCamera.SetActive(false);
        secondLandCamera.SetActive(true);

        Invoke("changeCameraToPlayer", 5.0f);
    }
    private void changeCameraToPlayer()
    {
        playerCamera.SetActive(true);
        secondLandCamera.SetActive(false);
    }

    public float getRemainingOil()
    {
        return remainingOil;
    }

    public void setRemainingOil(float value)
    {
        remainingOil += value;
        if (remainingOil > oilSlider.maxValue)
            remainingOil = oilSlider.maxValue +1;
    }
}
