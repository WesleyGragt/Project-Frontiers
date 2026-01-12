using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotation();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.linearVelocity = transform.forward * Input.GetAxis("Vertical") * speed
                            + transform.right * Input.GetAxis("Horizontal") * speed;
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up * mouseX);
    }
}
