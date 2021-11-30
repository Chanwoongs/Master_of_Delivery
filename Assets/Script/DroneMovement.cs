using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    Rigidbody ourDrone;

    // ����� ���� ��
    public float upForce;

    // ��� �յ� ������
    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;

    // ��� ȸ��
    private float wantedYRotation;
    public float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationYVelocity;

    // ��� �ӵ� ����
    private Vector3 velocityToSmoothDampToZero;

    // ��� �¿� ������
    private float sideMovementAmount = 300.0f;
    private float tiltAmountSide;
    private float tiltVelocitySide;

    // ȿ����
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

        // ��п��� �׻� ���� �ۿ�
        ourDrone.AddRelativeForce(Vector3.up * upForce);
        // ��� �յ� ȸ��
        ourDrone.rotation = Quaternion.Euler(new Vector3(tiltAmountForward, currentYRotation, tiltAmountSide));
    }

    private void DroneSound()
    {
        droneSound.pitch = 1 + (ourDrone.velocity.magnitude / 20);
    }

    // ��� �ϰ� �� ���ְ� �ϴ� ��
    private void MovementUpDown()
    {
        // �յ��¿� �Է��� ���� ��
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // ��� �Ǵ� �ϰ��� ��
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftControl))
            {
                // �ӵ� ����
                ourDrone.velocity = ourDrone.velocity;
            }
            // �յ��¿� �����Ӹ� ������
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 281;
            }
            // �յ��¿�� ȸ���� ���� ������ ��
            if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)))
            {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 110;
            }
            // ȸ��Ű�� ������ ��
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
            {
                upForce = 410;
            }
        }

        // �¿� Ű�� ������ ��
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            upForce = 135;
        }

        // ��� ���
        if (Input.GetKey(KeyCode.Space))
        {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
            {
                upForce = 500;
            }
        }
        // ��� �ϰ�
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            upForce = -200;
        }
        // �Է� ���� �� ��� ��ġ ����
        else if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f))
        {
            upForce = 98.1f;
        }
    }

    // �յڷ� ����
    private void MovementForward()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            // ������Ʈ ���� ��ǥ �������� ������ �̵� 
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            // �������� �ε巴�� ����̱�
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }

    // ȸ��
    private void Rotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            // ���� ȸ��
            wantedYRotation -= rotateAmountByKeys;
        }
        if (Input.GetKey(KeyCode.E))
        {
            // ������ ȸ��
            wantedYRotation += rotateAmountByKeys;
        }

        // �ε巴�� ȸ��
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    }

    // �ӵ� ����
    private void ClampingSpeedValues()
    {
        // �������� ����� ��
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        // �ڷ� ����� ��
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)
        {
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    // �¿� ����̱�
    private void Swerve()
    {
        // �¿� �Է��� ������
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            // ��� �̵�
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            // ��� ���� ����
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, -20 * Input.GetAxis("Horizontal"), ref tiltVelocitySide, 0.1f);
        }
        else
        {
            // �� ������
            tiltAmountSide = Mathf.SmoothDamp(tiltAmountSide, 0, ref tiltVelocitySide, 0.1f);
        }
    }

}
