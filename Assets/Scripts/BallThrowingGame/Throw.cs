using UnityEngine;

public class Throw : MonoBehaviour
{
    public static Throw HoldPoint;

    public bool pickup = false;

    private float chargeTime = 0f;
    [Space]
    [SerializeField] float maxChargeTime = 1000f;

    void Start()
    {
        HoldPoint = this;
    }

    private void Update()
    {
        if (PickUp.curBall == null) return;

        ChargeShot();
        ThrowBall();
    }

    private void ChargeShot()
    {
        if (Input.GetMouseButton(0))
        {
            if (chargeTime < maxChargeTime)
            {
                chargeTime += 5f;
            }
        }
    }

    private void ThrowBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (pickup)
            {
                if (PickUp.curBall == null) return;

                Rigidbody rb = PickUp.curBall.GetComponent<Rigidbody>();
                Collider col = PickUp.curBall.GetComponent<Collider>();
                PickUp.curBall.transform.parent = null;

                rb.isKinematic = false;
                col.enabled = true;
                rb.AddForce(transform.forward * chargeTime);

                PickUp.curBall = null;
                pickup = false;
                chargeTime = 0f;
            }
            else
            {
                pickup = true;
            }
        }
    }
}
