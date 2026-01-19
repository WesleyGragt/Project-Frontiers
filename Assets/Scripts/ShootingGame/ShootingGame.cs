using TMPro;
using UnityEngine;

public class ShootingGame : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] GameObject showUI;
    [SerializeField] TextMeshProUGUI bulletAmountText;
    [SerializeField] TextMeshProUGUI pointAmountText;
    [Space]
    [SerializeField] GameObject gunOnTable;
    [SerializeField] GameObject bullet;

    [SerializeField] private int maxBullets = 6;

    [HideInInspector]
    public int curBullets;

    private bool gameStarted = false;

    public static int pointsPerHit = 10;
    public static int shootingPoints = 0;

    private GameObject canvas;

    private void Start()
    {
        canvas = bulletAmountText.transform.parent.transform.parent.gameObject;
    }

    private void Update()
    {
        if (!gameStarted && gameObject.activeInHierarchy)
        {
            curBullets = maxBullets;
            gameStarted = true;

            canvas.SetActive(true);
            PlayerMovement.ableToMove = false;
        }

        AimAtCameraPos();
        Shooting();
    }

    private void AimAtCameraPos()
    {
        Ray ray = new Ray(camera.position, camera.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo);

        if (hitInfo.point == Vector3.zero) return;
        transform.LookAt(hitInfo.point);
    }

    private void Shooting()
    {
        pointAmountText.text = $"{shootingPoints}";

        if (Input.GetMouseButtonDown(0) && curBullets > 0)
        {
            GameObject instance = Instantiate(bullet, transform.position, transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(transform.forward * 3000f);

            curBullets--;
            bulletAmountText.text = $"{curBullets}";
        }

        if (curBullets == 0)
        {
            Debug.Log("Out of bullets!");


            gunOnTable.GetComponent<MeshRenderer>().enabled = true;
            gunOnTable.GetComponent<Collider>().enabled = true;
            gameObject.SetActive(false);

            gameStarted = false;
            PlayerMovement.ableToMove = true;
            
            if (showUI != null)
            {
                showUI.SetActive(true);
            }

            canvas.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void pointsToTickets()
    {
        TicketSystem.tickets = shootingPoints;
        shootingPoints = 0;
    }
}
