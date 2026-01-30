using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Rigidbody rb;
    Collider col;

    public static PickUp curBall;

    public bool Thrown = false;

    public float timeTillDestroy = 5f;

    [Header("Sounds")]
    public GameObject hitSound;
    public GameObject hitCanSound;

    bool hasHitCan;
    bool hasHitOther;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Can" && !hasHitCan)
        {
            hasHitCan = true;
            Instantiate(hitCanSound, transform.position, Quaternion.identity);
        }
        else if(hasHitOther)
        {
            hasHitOther = true;
            Instantiate(hitSound, transform.position, Quaternion.identity);
        }
    }
}