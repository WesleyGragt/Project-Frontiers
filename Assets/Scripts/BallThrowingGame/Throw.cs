using UnityEngine;
using UnityEngine.UI;

public class Throw : MonoBehaviour
{
    public static Throw HoldPoint;
    [SerializeField] Camera cam;
    [SerializeField] GameObject sliderUI;

    public bool pickup = false;
    [Space]
    [SerializeField] float maxChargeTime = 1000f;
    public GameObject throwSound;
    
    private float chargeTime = 0f;
    private bool startCharge = false;

    void Start()
    {
        HoldPoint = this;
    }

    private void Update()
    {
        if (PickUp.curBall == null) return;

        LookAtPoint();
        ChargeShot();
        ThrowBall();
    }

    private void ChargeShot()
    {
        sliderUI.SetActive(true);

        if (!startCharge) return;

        if (Input.GetMouseButton(0))
        {
            if (chargeTime < maxChargeTime)
            {
                chargeTime += 2f;
            }
        }

        sliderUI.GetComponent<Slider>().maxValue = maxChargeTime;
        sliderUI.GetComponent<Slider>().value = chargeTime;
    }

    private void ThrowBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (pickup)
            {
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

                startCharge = false;
            }
            else
            {
                pickup = true;
                startCharge = true;
            }
        }
    }

    private void LookAtPoint()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo)) return;

        if (hitInfo.point == Vector3.zero) return;
        transform.LookAt(hitInfo.point);
    }
}
