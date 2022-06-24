using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Look : MonoBehaviour
{
    public Camera cam;
    public Camera GunCam;
    private float xRotation = 0f;

    public float mouseX_ord;
    public float mouseY_ord;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        mouseX_ord = mouseX;
        mouseY_ord = mouseY;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, -90, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);

        if (GetComponent<PlayerSTAT>().Buffs.Contains(BuffList.ChipSightBuff))
        {
            cam.fieldOfView = 80;
            GunCam.fieldOfView = 80;
        }
        else
        {
            cam.fieldOfView = 60;
            GunCam.fieldOfView = 60;
        }
    }
}
