using TMPro;
using UnityEngine;

public class HammerManager : MonoBehaviour
{
    [SerializeField] private CameraLock camLock;
    [SerializeField] private GameObject showUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject ball;
    [Space]
    [SerializeField] private float heightToWin = 25f;
    [SerializeField] private int timeTillSmash = 5;
    [Space]
    [SerializeField] private float powerPerTap = 1f;
    [SerializeField] private float powerLossPerSec = 2f;

    private Rigidbody ballRB;

    private Collider col;

    private bool gameStarted = false;
    private bool startTimer = false;

    private float currentPower = 0f;

    private float curTime;
    private float timeOfStart;

    private int currentPoints = 0;
    [SerializeField] private int pointsGain = 100;

    private bool smashedBall = false;

    private float maxBallHeight = 0f;
    private Animator hammerPivot;

    private void Start()
    {
        hammerPivot = hammer.transform.parent.GetComponent<Animator>();

        col = gameObject.GetComponent<Collider>();
        ballRB = ball.GetComponent<Rigidbody>();
    }

    public void StartGame()
    {
        if (gameStarted) return;

        col.enabled = false;
        timerText.transform.parent.gameObject.SetActive(true);
        timerText.text = $"{timeTillSmash}:00";

        camLock.LockCamera();
        hammerPivot.enabled = false;

        hammer.GetComponent<MeshRenderer>().enabled = true;

        gameStarted = true;
    }

    private void Update()
    {
        if (!gameStarted) return;

        if (!startTimer && Input.GetKeyDown(KeyCode.Space))
        {
            startTimer = true;

            timeOfStart = Time.time;
            curTime = Time.time;
        }

        if (!startTimer) return;

        if (!hammerPivot.enabled)
        {
            powerUp();
        }

        if (Time.time > timeOfStart + timeTillSmash)
        {
            if (!smashedBall)
            {
                ballRB.AddForce(Vector3.up * currentPower, ForceMode.Impulse);
                
                hammerPivot.enabled = true;
                hammerPivot.Play("HammerSmash", -1, 0);

                smashedBall = true;
                
                if (showUI != null)
                {
                    showUI.SetActive(true);
                }
                
                timerText.text = "0:00";
            }

            BallHeight();
        }
        else
        {
            int secText = Mathf.FloorToInt(timeOfStart + timeTillSmash - Time.time);
            int miliSecText = Mathf.FloorToInt((timeOfStart + timeTillSmash - Time.time - secText) * 100);
            timerText.text = $"{secText}:{miliSecText}";
        }

    }

    private void powerUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPower += powerPerTap;
        }

        currentPower -= powerLossPerSec * (Time.time - curTime);
        curTime = Time.time;

        if (currentPower < 0) currentPower = 0;

        Debug.Log(currentPower);
    }

    private void BallHeight()
    {
        if (currentPower == 0) resetGame();

        if (ball.transform.position.y > maxBallHeight)
        {
            maxBallHeight = ball.transform.position.y;
        }

        if (ball.transform.position.y < maxBallHeight || ball.transform.position.y > heightToWin)
        {
            ballRB.linearVelocity = ballRB.linearVelocity * -0.1f;

            CheckHeight();
        }
    }

    private void CheckHeight()
    {
        if (maxBallHeight > heightToWin)
        {
            currentPoints += pointsGain;
            Debug.Log($"Reached required height, gained {pointsGain} points. New total: {currentPoints}");

            resetGame();
        }
        else if (ball.transform.position.y < maxBallHeight)
        {
            Debug.Log("Failed to reach Height");

            resetGame();
        }
    }

    private void resetGame()
    {
        currentPower = 0;
        maxBallHeight = 0;
        startTimer = false;
        gameStarted = false;
        smashedBall = false;

        col.enabled = true;
        timerText.transform.parent.gameObject.SetActive(false);
    }

    public void pointsToTickets()
    {
        TicketSystem.tickets = currentPoints;
        currentPoints = 0;
    }
}
