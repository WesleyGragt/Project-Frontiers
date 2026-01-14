using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Rigidbody rb;
    Collider col;

    public static PickUp curBall;

    public bool Thrown = false;

    public float timeTillDestroy = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Thrown)
        {
            Destroy(gameObject, timeTillDestroy);
        }
    }

    public void GrabBall()
    {
        if (curBall != null) return;
        if (Thrown) return;

        curBall = this;
        rb.isKinematic = true;
        col.enabled = false;

        transform.position = Throw.HoldPoint.transform.position;
        transform.parent = Throw.HoldPoint.transform;
    }
}