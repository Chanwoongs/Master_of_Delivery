using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneTutorial : MonoBehaviour
{
    Rigidbody ourDrone;

    // 위로 뜨는 힘
    public float upForce;

    // 앞뒤 움직임
    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;

    // 회전
    private float wantedYRotation;
    public float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationYVelocity;

    // 속도 제한
    private Vector3 velocityToSmoothDampToZero;

    // 좌우 움직임
    private float sideMovementAmount = 300.0f;
    private float tiltAmountSide;
    private float tiltVelocitySide;

    // 오디오 효과음
    private AudioSource droneSound;

    void Awake()
    {
        ourDrone = GetComponent<Rigidbody>();
        droneSound = gameObject.transform.Find("DroneSound").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();
        Swerve();
        DroneSound();

        if (transform.localPosition.y > 20)
        {
            transform.position = new Vector3(transform.position.x, 20, transform.position.z);
        }
        if (transform.localPosition.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        // 뜨는 힘 적용
        ourDrone.AddRelativeForce(Vector3.up * upForce * 49.55f * Time.deltaTime);
        // 속도 제어
        ourDrone.velocity = ourDrone.velocity / 1.2f;
        // 회전 적용
        ourDrone.rotation = Quaternion.Euler(new Vector3(tiltAmountForward, currentYRotation, tiltAmountSide));
    }

    private void DroneSound()
    {
            droneSound.pitch = 1 + (ourDrone.velocity.magnitude / 20);
    }

    // 상하 움직임
    private void MovementUpDown()
    {
        // 상하좌우 입력이 있을 때
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // Space나 Left Control 눌렀을 때
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl))
            {
                // 속도 제한
                ourDrone.velocity = ourDrone.velocity;
            }
            // 상하좌우 입력만 있을 때
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 281;
            }
            // 회전만 있을 때
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 110;
            }
            // 다른 키와 회전이 있을 때
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            {
                upForce = 410;
            }
        }

        // 좌우 움직임만 있을 때
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }

        // 상승할 때
        if (Input.GetKey(KeyCode.Space))
        {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 500;
            }
        }
        // 하강할 때
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            upForce = -200;
        }
        // 모든 입력이 없을 때
        else if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

    // 앞뒤 움직임
    private void MovementForward()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            // 힘 적용
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed * 0.5f);
            // 부드럽게 기울이기
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }

    // 회전
    private void Rotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            // 왼쪽 회전
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.E))
        {
            // 오른쪽 회전
            wantedYRotation += rotateAmountByKeys;
        }

        // 부드럽게 회전
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    }

    // 속도 제어
    private void ClampingSpeedValues()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    // 흔들림
    private void Swerve()
    {
        // 좌우 입력이 있을 때
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // 속도
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            // 기울이기
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, -20 * Input.GetAxis("Horizontal"), ref tiltVelocitySide, 0.1f);
        }
        else
        {
            // 제자리로
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, 0, ref tiltVelocitySide, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("MainScene");
    }
}