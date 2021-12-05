using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarTutorialController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private Vector3 centerOfMass;   

    [SerializeField] private float motorForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    public AudioClip carStartAudio;
    public AudioClip forward1Audio;
    public AudioClip forward2Audio;

    public AudioSource carStartAS;
    public AudioSource forwardAS;

    public Rigidbody rb;

    public float currentSpeed;
    public float currentLSpeed;
    public float currentRSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        centerOfMass = new Vector3(0, -0.2f, 0);
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    }
    private void initailizeAudio()
    {
        carStartAS = gameObject.AddComponent<AudioSource>();
        forwardAS = gameObject.AddComponent<AudioSource>();

        carStartAS.clip = carStartAudio;
        forwardAS.clip = forward1Audio;

        carStartAS.playOnAwake = false;
        forwardAS.playOnAwake = false;
    }
    private void updateAudio()
    {
        if (currentSpeed >= 0 && currentSpeed < 40)
        {
            forwardAS.clip = forward1Audio;
        }
        else if (currentSpeed >= 40)
        {
            forwardAS.clip = forward2Audio;
        }
        if (forwardAS.isPlaying) return;
        forwardAS.Play();
    }
    private void Update()
    {    
        updateAudio();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity *= 0.9f;
        }

        currentLSpeed = 2 * 3.14f * rearLeftWheelCollider.radius * rearLeftWheelCollider.rpm * 60 / 1000;
        currentRSpeed = 2 * 3.14f * rearRightWheelCollider.radius * rearRightWheelCollider.rpm * 60 / 1000;
        currentSpeed = (currentLSpeed + currentRSpeed) / 2.0f;
        currentSpeed = Mathf.Round(currentSpeed);
    }

    private void HandleMotor()
    {
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
    }
 
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
    }
    public void setMotorForce(float motorForce)
    {
        this.motorForce = motorForce;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
            SceneManager.LoadScene("DroneTutorial");
    }
}
