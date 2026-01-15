using UnityEngine;
using UnityEngine.Rendering.UI;

public class HammerManager : MonoBehaviour
{
    [SerializeField] private CameraLock camLock;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject ball;
    [Space]
    [SerializeField] private float heightToWin = 25f;
    [SerializeField] private float timeTillSmash = 5;
    [Space]
    [SerializeField] private float powerPerTap = 1f;
    [SerializeField] private float powerLossPerSec = 2f;

    Rigidbody ballRB;

    private bool gameStarted = false;
    private bool startTimer = false;

    private float currentPower = 0f;

    private float curTime;
    private float timeOfStart;

    private float currentPoints = 0f;
    [SerializeField] private float pointsGain = 100f;

    private float maxBallHeight = 0f;

    private void Start()
    {
        ballRB = ball.GetComponent<Rigidbody>();
    }

    public void StartGame()
    {
        hammer.SetActive(true);
        hammer.GetComponent<Animator>().enabled = false;
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

        if (startTimer && !hammer.GetComponent<Animator>().enabled)
        {
            powerUp();
        }
     
        if (startTimer && Time.time > timeOfStart + timeTillSmash)
        {
            ballRB.AddForce(Vector3.up * currentPower, ForceMode.Impulse);

            hammer.GetComponent<Animator>().enabled = true;

            currentPower = 0;
            
            BallHeight();
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
        if (ball.transform.position.y > maxBallHeight)
        {
            maxBallHeight = ball.transform.position.y;
        }

        if (ball.transform.position.y < maxBallHeight || ball.transform.position.y > heightToWin)
        {
            ballRB.linearVelocity = ballRB.linearVelocity * -0.5f;

            CheckHeight();
        }
    }

    private void CheckHeight()
    {
        if (maxBallHeight > heightToWin)
        {
            currentPoints += pointsGain;
            Debug.Log($"Reached required height, gained {pointsGain} points. New total: {currentPoints}");
        }
        else if (ball.transform.position.y < maxBallHeight)
        {
            Debug.Log("Failed to reach Height");
        }

        maxBallHeight = 0;
        gameStarted = false;
        startTimer = false;
    }
}
