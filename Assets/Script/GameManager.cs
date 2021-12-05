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

    // 남은 음식 UI
    public Image food1;
    public Image food2;
    public Image food3;
    public Image food4;
    public Image food5;
    public Image food6;
    public Image food7;

    // 드론 UI 부모
    public GameObject droneUI;
    // 드론 hp UI
    public Slider droneHP;
    public Text hpTxt;
    // 드론 높이 UI
    public Slider droneHeight;
    public Text heightTxt;

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

    // 내륙 지역을 클리어 했는지
    public bool isFirstCleared;

    public GameObject playerCamera;
    public GameObject secondLand;
    public GameObject secondLandCamera;

    public GameObject pickableDrones;

    GameObject escMenu;

    private GameObject player;
    public GameObject drone;
    public GameObject pickableDrone;
    Vector3 target;
    public UnityEvent toSecondLand;
    Drone d;
    PickableDrone pd;

    // 지진소리
    public AudioSource earthquackSound;
    public AudioSource bgm;

    CarController cc;

    public CameraMove cam;
    public AudioSource orderAudio;
    GameObject explainDroneText;
    GameObject hambergerText;

    private void Start()
    {
        pd = pickableDrone.GetComponent<PickableDrone>();
        d = drone.GetComponent<Drone>();
        player = GameObject.Find("Player");

        isDeliverd1 = 0;
        isDeliverd2 = -1;
        isDeliverd3 = -1;
        isDeliverd4 = -1;
        isDeliverd5 = -1;
        isDeliverd6 = -1;
        isDeliverd7 = -1;

        // 2번째 배달 지역 음식 UI 끄기
        food4.gameObject.SetActive(false);
        food5.gameObject.SetActive(false);
        food6.gameObject.SetActive(false);
        food7.gameObject.SetActive(false);

        // 초기 시간 설정
        time = 150.0f;
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

        escMenu = GameObject.Find("UICanvas").transform.GetChild(4).gameObject;
        explainDroneText = GameObject.Find("UICanvas").transform.GetChild(8).gameObject;
        hambergerText = GameObject.Find("UICanvas").transform.GetChild(6).gameObject;
    }

    void Update()
    {
        gameClear();
        updateTime();
        updateDestination();

        if (isInCar)
        {
            UpdateOilgaGage();
        }
        UpdateDroneHP();
        UpdateDroneHeight();
        UpdateEscKey();
    }

    // 배달지 업데이트
    private void updateDestination()
    {
        if (isDeliverd2 == 0)
        {
            GameObject.Find("Section2").transform.GetChild(randNum2).gameObject.SetActive(true);
            food1.gameObject.SetActive(false);
        }
        if (isDeliverd3 == 0)
        {
            GameObject.Find("Section3").transform.GetChild(randNum3).gameObject.SetActive(true);
            food2.gameObject.SetActive(false);
        }
        if (isDeliverd4 == 0)
        {
            isFirstCleared = true;
            secondLand.transform.position = Vector3.MoveTowards(secondLand.transform.position, target, 4.5f * Time.deltaTime);
            if (toSecondLand != null)
            {
                toSecondLand.Invoke();
            }
            toSecondLand = null;
            GameObject.Find("Section4").transform.GetChild(randNum4).gameObject.SetActive(true);

            // UI 업데이트
            food3.gameObject.SetActive(false);
            food4.gameObject.SetActive(true);
            food5.gameObject.SetActive(true);
            food6.gameObject.SetActive(true);
            food7.gameObject.SetActive(true);

            // 드론 배치
            pickableDrones.SetActive(true);
            
        }
        if (isDeliverd5 == 0)
        {
            GameObject.Find("Section6").transform.GetChild(randNum6).gameObject.SetActive(true);
            food4.gameObject.SetActive(false);
        }
        if (isDeliverd6 == 0)
        {
            GameObject.Find("Section5").transform.GetChild(randNum5).gameObject.SetActive(true);
            food6.gameObject.SetActive(false);
        }
        if (isDeliverd7 == 0)
        {
            GameObject.Find("Section7").transform.GetChild(randNum7).gameObject.SetActive(true);
            food5.gameObject.SetActive(false);
        }
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
        remainingOil -= 1.2f * Time.deltaTime;
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

    private void UpdateDroneHP()
    {
        if (pd.isControlling)
        {
            droneHP.value = d.hp;
            hpTxt.text = d.hp.ToString() + "%";
        }
    }

    private void UpdateDroneHeight()
    {
        if (pd.isControlling)
        {
            droneHeight.value = d.height;
            heightTxt.text = d.height.ToString("F2");
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
        earthquackSound.Play();
        bgm.Stop();
        secondLand.transform.position = new Vector3(secondLand.transform.position.x, -40f, secondLand.transform.position.z);
        playerCamera.SetActive(false);
        secondLandCamera.SetActive(true);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1000, player.transform.position.z);

        Invoke("changeCameraToPlayer", 12.0f);
    }
    private void changeCameraToPlayer()
    {
        earthquackSound.Stop();
        bgm.Play();
        player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        playerCamera.SetActive(true);
        secondLandCamera.SetActive(false);

        orderAudio.Play();
        explainDroneText.SetActive(false);
        hambergerText.SetActive(true);
    }

    public void resetTime()
    {
        time = 200;
    }

    public float getRemainingOil()
    {
        return remainingOil;
    }

    public void setRemainingOil(float value)
    {
        remainingOil += value;
        if (remainingOil > oilSlider.maxValue)
            remainingOil = oilSlider.maxValue + 1;
    }

    void UpdateEscKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escMenu.activeSelf)
            {
                escMenu.SetActive(true);
                cam.setIsAction(false);
                Time.timeScale = 0;
            }
            else
            {
                escMenu.SetActive(false);
                cam.setIsAction(true);
                Time.timeScale = 1;
            }
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GameQuit()
    {
        Application.Quit();
    }

    public void ShowExplainDroneText()
    {
        explainDroneText.SetActive(true);
    }
}