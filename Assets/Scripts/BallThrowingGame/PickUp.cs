using UnityEngine;

public class PickUp : MonoBehaviour
{
    Rigidbody rb;
    Collider col;

    public static PickUp curBall;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void GrabBall()
    {
        if (curBall != null) return;
        
        curBall = this;
        rb.isKinematic = true;
        col.enabled = false;

        transform.position = Throw.HoldPoint.transform.position;
        transform.parent = Throw.HoldPoint.transform;
    }
}