using System;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookUpDown();
    }

    private void LookUpDown()
    {
        // Get current local X rotation in -180..180 range
        Vector3 euler = transform.localEulerAngles;
        float currentX = euler.x;
        if (currentX > 180f) currentX -= 360f;

        float newX = currentX - Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(Mathf.Clamp(newX, -20f, 50f), 0, 0);
    }
}
