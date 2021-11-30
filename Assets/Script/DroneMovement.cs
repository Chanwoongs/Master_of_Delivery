using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    Rigidbody ourDrone;

    // 드론의 비행 힘
    public float upForce;

    // 드론 앞뒤 움직임
    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;

    // 드론 회전
    private float wantedYRotation;
    public float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationYVelocity;

    // 드론 속도 제한
    private Vector3 velocityToSmoothDampToZero;

    // 드론 좌우 움직임
    private float sideMovementAmount = 300.0f;
    private float tiltAmountSide;
    private float tiltVelocitySide;

    // 효과음
    private AudioSource droneSound;

    void Awake()
    {
        ourDrone = GetComponent<Rigidbody>();
        droneSound = gameObject.transform.FindChild("DroneSound").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        MovementUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();
        Swerve();
        DroneSound();

        // 드론에겐 항상 힘이 작용
        ourDrone.AddRelativeForce(Vector3.up * upForce);
        // 드론 앞뒤 회전
        ourDrone.rotation = Quaternion.Euler(new Vector3(tiltAmountForward, currentYRotation, tiltAmountSide));
    }

    private void DroneSound()
    {
        droneSound.pitch = 1 + (ourDrone.velocity.magnitude / 20);
    }

    // 상승 하강 및 떠있게 하는 힘
    private void MovementUpDown()
    {
        // 앞뒤좌우 입력이 있을 때
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // 상승 또는 하강할 때
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl))
            {
                // 속도 제한
                ourDrone.velocity = ourDrone.velocity;
            }
            // 앞뒤좌우 움직임만 있을때
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 281;
            }
            // 앞뒤좌우와 회전을 같이 눌렀을 때
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 110;
            }
            // 회전키를 눌렀을 때
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            {
                upForce = 410;
            }
        }

        // 좌우 키만 눌렀을 때
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }

        // 드론 상승
        if (Input.GetKey(KeyCode.Space))
        {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 500;
            }
        }
        // 드론 하강
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            upForce = -200;
        }
        // 입력 없을 시 드론 위치 유지
        else if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

    // 앞뒤로 기울기
    private void MovementForward()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            // 오브젝트 로컬 좌표 기준으로 앞으로 이동 
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            // 앞쪽으로 부드럽게 기울이기
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

    // 속도 제한
    private void ClampingSpeedValues()
    {
        // 앞쪽으로 기울일 때
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        // 뒤로 기울일 때
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    // 좌우 기울이기
    private void Swerve()
    {
        // 좌우 입력을 받으면
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // 드론 이동
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            // 드론 기울기 각도
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, -20 * Input.GetAxis("Horizontal"), ref tiltVelocitySide, 0.1f);
        }
        else
        {
            // 안 기울어짐
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, 0, ref tiltVelocitySide, 0.1f);
        }
    }

}
