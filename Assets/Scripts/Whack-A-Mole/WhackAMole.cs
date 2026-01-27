using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WhackAMole : MonoBehaviour
{
    [Header("Components")]
    public Mole[] moles;
    public CameraLock cameraLock;
    public IntVariable coins;
    public GameObject uiHide;
    public MoleHammer hammer;

    [Header("Settings")]
    public float gameTime;
    public float curGameTime;

    [Space(20)]
    public float timeBetweenMolePopup;
    float curTimeBetweenMolePopup;
    float timeElapsedBetweenMolePopup;
    [Range(0f, 1f)]
    public float timeBetweenMolePopupMult;

    [Space(20)]
    public float moleUptime;
    float curMoleUptime;
    [Range(0f,1f)]
    public float moleUptimeMult;

    [Space(20)]
    public float timePerMoleCountIncrease;
    float timeSinceMoleCountIncrease;
    public int moleCount = 1;

    [Space(20)]
    public int pointsPerMole;
    public int pointsToTicketsConversion;

    [Header("Results")]
    public int points;
    public int amountedPoints;

    [Header("States")]
    public bool started;

    [Header("UI")]
    public Text timeText;
    public Text pointsText;



    private void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        if(started)
        {
            //Pop up moles after some time
            timeElapsedBetweenMolePopup += Time.deltaTime;
            if (timeElapsedBetweenMolePopup > curTimeBetweenMolePopup)
            {
                for (int i = 0; i < Mathf.Min(moles.Length - 1, Random.Range(1, moleCount)); i++)
                {
                    PopUpMoles();
                }
                timeElapsedBetweenMolePopup = 0;
                curTimeBetweenMolePopup *= timeBetweenMolePopupMult;
                curMoleUptime *= moleUptimeMult;
            }

            //Increase mole count after some time
            timeSinceMoleCountIncrease += Time.deltaTime;
            if(timeSinceMoleCountIncrease > timePerMoleCountIncrease)
            {
                timeSinceMoleCountIncrease = 0;
                moleCount++;
            }
            
            
            //End game after some time
            curGameTime -= Time.deltaTime;
            UpdateUI();
            if (curGameTime < 0)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        if(!started && coins.value >= 1)
        {
            started = true;
            coins.Decrease();
            hammer.SetIdle();
            cameraLock.LockCamera();
            uiHide.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    void PopUpMoles()
    {
        int randomMole = Random.Range(0, moles.Length - 1);
        if (moles[randomMole].isUp)
        {
            PopUpMoles();
            return;
        }
        else
        {
            moles[randomMole].PopUp(curMoleUptime);
        }

    }


    public void GainPoints()
    {
        points += pointsPerMole;
        //add ux things
        UpdateUI();
    }

    public void LosePoints()
    {
        points -= pointsPerMole;
        //add ux things
        UpdateUI();
    }


    public void PointToTickets()
    {
        TicketSystem.tickets = amountedPoints / pointsToTicketsConversion;
        points = 0;
    }
    

    void UpdateUI()
    {
        string pointsStr = points.ToString("#");
        if(points == 0)
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

        string timeStr = curGameTime.ToString("#");
        if (timeStr.Length == 2)
        {
            timeStr = "0" + timeStr;
        }
        else if (timeStr.Length == 1)
        {
            timeStr = "00" + timeStr;
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

        string updatedTimeStr = "";
        foreach (char character in timeStr)
        {
            if (character == '1')
            {
                updatedTimeStr += " " + character;
            }
            else
            {
                updatedTimeStr += character;
            }
        }

        pointsText.text = updatedPointsString;
        timeText.text = updatedTimeStr;
    }


    public void SetHammerWhack(Vector3 whackPos)
    {
        hammer.moleWhackHammerPos.position = whackPos + hammer.moleHammerOffset;
        hammer.SetWhack();
    }


    public void EndGame()
    {
        ResetGame();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraLock.UnlockCamera();
        uiHide.SetActive(true);
        hammer.PutDown();

        //add ux things
    }


    public void ResetGame()
    {
        for (int i = 0; i < moles.Length; i++)
        {
            moles[i].Reset();
        }
        started = false;
        if (points > 0)
        {
            amountedPoints += points;
        }
        points = 0;
        moleCount = 1;
        curGameTime = gameTime;
        timeSinceMoleCountIncrease = 0;
        timeElapsedBetweenMolePopup = timeBetweenMolePopup * .8f;
        curMoleUptime = moleUptime;
        curTimeBetweenMolePopup = timeBetweenMolePopup;
        pointsText.text = "";
        timeText.text = "";

    }
}
