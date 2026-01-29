using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootingGame : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] GameObject showUI;
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] Text pointText;
    [Space]
    [SerializeField] GameObject gunOnTable;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shotEffect;

    [SerializeField] private int maxBullets = 6;

    [HideInInspector]
    public int curBullets;

    private bool gameStarted = false;

    public static int pointsPerHit = 1;
    public static int shootingPoints = 0;

    private GameObject canvas;
    private string startBulletText;
    private string startPointText;

    private void Start()
    {
        canvas = bulletText.transform.parent.gameObject;
        startBulletText = bulletText.text;
        startPointText = pointText.text;
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

        if (lookingAtNpc()) return;
        Shooting();
    }

    private void AimAtCameraPos()
    {
        Ray ray = new Ray(camera.position, camera.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo);

        if (hitInfo.point == Vector3.zero) return;
        transform.LookAt(hitInfo.point);
    }

    private bool lookingAtNpc()
    {
        Ray ray = new Ray(camera.position, camera.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo);

        if (hitInfo.transform.parent.name == "NPCS") return true;

        return false;
    }

    private void Shooting()
    {
        UpdateUI();
        bulletText.text = $"{startBulletText} {curBullets}";
        
        if (Input.GetMouseButtonDown(0) && curBullets > 0)
        {
            Vector3 smokePos = transform.position + transform.forward * 0.5f;
            GameObject gunEffect = Instantiate(shotEffect, smokePos, transform.rotation);
            gunEffect.transform.localScale *= 0.5f;

            GameObject instance = Instantiate(bullet, transform.position, transform.rotation);
            instance.GetComponent<Rigidbody>().AddForce(transform.forward * 3000f);

            curBullets--;
        }

        if (curBullets == 0)
        {
            Debug.Log("Out of bullets!");

            gunOnTable.gameObject.SetActive(true);

            gunOnTable.GetComponent<Collider>().enabled = true;
            gameObject.SetActive(false);

            gameStarted = false;
            PlayerMovement.ableToMove = true;
            
            if (showUI != null)
            {
                showUI.SetActive(true);
            }

            curBullets = maxBullets;

            canvas.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void pointsToTickets()
    {
        TicketSystem.tickets = shootingPoints;
        shootingPoints = 0;
        pointText.text = "";
    }


    void UpdateUI()
    {
        string pointsStr = shootingPoints.ToString("#");
        if (shootingPoints == 0)
        {
            pointsStr = "000";
        }
        else
        {
            if (pointsStr.Length == 2)
            {
                pointsStr = "0" + pointsStr;
            }
            else if (pointsStr.Length == 1)
            {
                pointsStr = "00" + pointsStr;
            }
        }

        string updatedPointsString = "";
        foreach (char character in pointsStr)
        {
            if (character == '1')
            {
                updatedPointsString += " " + character;
            }
            else
            {
                updatedPointsString += character;
            }
        }


        pointText.text = updatedPointsString;
    }
}
