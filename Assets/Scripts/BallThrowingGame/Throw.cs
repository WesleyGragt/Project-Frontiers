using UnityEngine;
using UnityEngine.UI;

public class Throw : MonoBehaviour
{
    public static Throw HoldPoint;
    [SerializeField] GameObject sliderUI;

    public bool pickup = false;
    [Space]
    [SerializeField] float maxChargeTime = 1000f;
    
    private float chargeTime = 0f;

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

        sliderUI.SetActive(true);
        sliderUI.GetComponent<Slider>().maxValue = maxChargeTime;
        sliderUI.GetComponent<Slider>().value = chargeTime;
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

                PickUp.curBall.GetComponent<InstantiateEffect>().enabled = true;

                PickUp.curBall.Thrown = true;
                PickUp.curBall = null;
                pickup = false;

                chargeTime = 0f;
                sliderUI.SetActive(false);
            }
            else
            {
                pickup = true;
            }
        }
    }
}
