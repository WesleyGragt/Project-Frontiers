using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float minimumRotationX = -20f;
    [SerializeField] private float maximumRotationX = 50f;
    public Camera camera;
    public PlayerMovement player;
    public bool isLocked = false;
    public GameObject mouseIcon;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!isLocked)
        {
            LookUpDown();
            InteractWithObject();
        }

        WhackAMoleControls();
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
            if (hitInfo.transform.tag != "Interactable")
            {
                mouseIcon.SetActive(false);
                return;
            }

            mouseIcon.SetActive(true);
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null && Input.GetMouseButtonDown(0))
            {
                interactable.Interact();
            }
        }
        else
        {
            mouseIcon.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }

    void WhackAMoleControls()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = camera.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Mole")
                {
                    hit.transform.GetComponent<Mole>().Whack();
                }
            }
        }
    }
}
