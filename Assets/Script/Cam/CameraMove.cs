using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    public float cameraSoothingFactor = 1;
    public float lookUpMax = 60;
    public float lookUpMin = -60;
    public Transform playerCamera;

    private Quaternion camRotation;
    private RaycastHit hit;
    private Vector3 cameraOffset;
    bool isAction = true;

    private void Start()
    {
        camRotation = transform.rotation;
        cameraOffset = playerCamera.localPosition;
    }
    private void Update()
    {
        if (!isAction)
            return;

        camRotation.x += Input.GetAxis("Mouse Y") * cameraSoothingFactor * (-1);
        camRotation.y += Input.GetAxis("Mouse X") * cameraSoothingFactor;

        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

        if (Physics.Linecast(transform.position, transform.position + transform.localRotation * cameraOffset, out hit))
        {
            playerCamera.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, cameraOffset, Time.deltaTime);
        }
    }

    public void setIsAction(bool action)
    {
        isAction = action;
    }
}