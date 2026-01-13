using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float minimumRotationX = -20f;
    [SerializeField] private float maximumRotationX = 50f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookUpDown();
        InteractWithObject();
    }

    private void LookUpDown()
    {
        // Get current local X rotation in -180..180 range
        Vector3 euler = transform.localEulerAngles;
        float currentX = euler.x;
        if (currentX > 180f) currentX -= 360f;

        float newX = currentX - Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(Mathf.Clamp(newX, minimumRotationX, maximumRotationX), 0, 0);
    }

    private void InteractWithObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2f))
        {
            if (hitInfo.transform.tag != "Interactable") return;

            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null && Input.GetMouseButtonDown(0))
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}
