using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public WhackAMole whackAMole;
    public Transform topLocation;
    public Transform bottomLocation;
    public LerpTo lerp;
    [Header("States")]
    public bool isUp;

    public void Whack()
    {
        if (isUp)
        {
            whackAMole.GainPoints();
            Reset();
            StopCoroutine(ResetAfterTime(0));

            //add ux things
        }
    }

    public void Reset()
    {
        isUp = false;
        lerp.target = bottomLocation;
    }

    public void PopUp(float time)
    {
        if (!isUp)
        {
            isUp = true;
            lerp.target = topLocation;
            StartCoroutine(ResetAfterTime(time));
        }
    }

    IEnumerator ResetAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Reset();
    }
}
