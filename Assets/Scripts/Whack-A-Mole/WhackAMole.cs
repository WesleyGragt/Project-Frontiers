using UnityEngine;

public class WhackAMole : MonoBehaviour
{
    [Header("Components")]
    public Mole[] moles;
    public CameraLock cameraLock;

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

    [Header("States")]
    public bool started;


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
            curGameTime += Time.deltaTime;
            if(curGameTime > gameTime)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        if(!started)
        {
            started = true;
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
    }


    public void EndGame()
    {
        ResetGame();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraLock.UnlockCamera();

        //add tickets to total tickets
        //add ux things
    }


    public void ResetGame()
    {
        for (int i = 0; i < moles.Length; i++)
        {
            moles[i].Reset();
        }
        started = false;
        moleCount = 1;
        curGameTime = 0;
        timeSinceMoleCountIncrease = 0;
        timeElapsedBetweenMolePopup = timeBetweenMolePopup * .8f;
        curMoleUptime = moleUptime;
        curTimeBetweenMolePopup = timeBetweenMolePopup;
        points = 0;

    }
}
