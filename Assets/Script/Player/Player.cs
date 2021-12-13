using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    public bool m_jumpInput = false;

    [SerializeField]  private bool m_isGrounded;

    private List<Collider> m_collisions = new List<Collider>();

    public GameObject playerCamera;
    public GameObject carCamera;
    public GameObject playerCar;
    public GameObject player;
    public GameObject drone;
    public GameObject minimapDrone;
    public GameObject pickableDrone;
    public GameObject droneCamera;
    public GameObject miniMap;
    public GameObject candy;
    public Collider c;
    public Canvas canvas;
    CarController cc;
    Drone d;
    PickableDrone pd;
    public bool hasCandy;

    public AudioSource playerAudio;
    public AudioSource interactAudio;

    public AudioClip footStep;
    public AudioClip jump;
    public AudioClip getCandy;
    public AudioClip deliverd;
    public AudioClip boom;
    GameManager gameManager;
    bool isMove = true;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
        candy.transform.localScale = new Vector3(0, 0, 0);
    }
    private void Start()
    {
        playerAudio = gameObject.GetComponent<AudioSource>();
        c = gameObject.GetComponent<CapsuleCollider>();
        candy = GameObject.Find("PlayerCandy");
        cc = GameObject.Find("DeliveryCar").GetComponent<CarController>();
        d = drone.GetComponent<Drone>();
        pd = pickableDrone.GetComponent<PickableDrone>();
        m_jumpInput = false;
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    private void Update()
    {
        if (!m_jumpInput && !cc.isDriving && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (!miniMap.activeSelf)
            {
                miniMap.SetActive(true);
            }
            else if (miniMap.activeSelf)
            {
                miniMap.SetActive(false);
            }
        }
    }
    private void PlayFootStepSound()
    {
        if (cc.isDriving) return;
        playerAudio.volume = 0.5f;
        playerAudio.clip = footStep;
        playerAudio.Play();
    }
    private void PlayJumpSound()
    {
        playerAudio.volume = 0.5f;
        playerAudio.clip = jump;
        playerAudio.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Asphalt"))
        {
            m_isGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Asphalt"))
        {
            m_isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_isGrounded = false; 
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (cc.isDriving)
                    return;

                cc.carStartAS.Play();
                
                playerCamera.SetActive(false);
                carCamera.SetActive(true);
                c.enabled = false;
                player.transform.position = playerCar.transform.position;
                player.transform.localScale = new Vector3(0, 0, 0);
                player.GetComponent<Animator>().enabled = false;
                Invoke("setDrivingTrue", 0.2f);
            }
        }
        if (other.CompareTag("PickableDrone"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (pd.isControlling) return;

                playerCamera.SetActive(false);

                droneCamera.SetActive(true);
                drone.SetActive(true);
                drone.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);

                player.transform.position = player.transform.position;
                player.GetComponent<Animator>().enabled = false;

                gameManager.droneUI.gameObject.SetActive(true);
                other.gameObject.SetActive(false);
                Invoke("setControllingTrue", 0.2f);
            }
        }
    }

    private void setControllingTrue()
    {
        pd.isControlling = true;
    }

    private void setDrivingTrue()
    {
        cc.isDriving = true;
        gameManager.SetIsInCar(true);  // 차타고 0.2초뒤에 움직이니까 그 때 기름 감소하도록
    }

    private void FixedUpdate()
    {
        if (cc.isDriving || pd.isControlling) return;

        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    private void TankUpdate()
    {
        if(isMove)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

            m_animator.SetFloat("MoveSpeed", m_currentV);

            JumpingAndLanding();
        }
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_jumpInput = false;
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

    public void SetIsMove(bool move)
    {
        isMove = move;
    }
}
