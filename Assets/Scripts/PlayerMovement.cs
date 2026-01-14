using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Rigidbody rb;

    private float gravity = 0;
    public bool isLocked = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isLocked)
        {
            Rotation();
        }
    }

    private void FixedUpdate()
    {
        if (!isLocked)
        {
            Movement();
            notGrounded();
        }
    }

    private void Movement()
    {
        rb.linearVelocity = transform.forward * Input.GetAxis("Vertical") * speed
                            + transform.right * Input.GetAxis("Horizontal") * speed
                            + -transform.up * gravity;
    }

    private void notGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (!Physics.Raycast(ray, 1.1f))
        {
            gravity = 3;
        }
        else gravity = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * 1.1f);
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * mouseX);
    }
}
