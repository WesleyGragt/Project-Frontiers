using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public WhackAMole whackAMole;
    public Transform topLocation;
    public Transform bottomLocation;
    public LerpTo lerp;
    public GameObject bomb;
    [Range(0f,1f)]
    public float bombChance;
    [Header("States")]
    public bool isUp;
    public bool hasBomb = false;

    public void Whack()
    {
        if (isUp)
        {
            if (hasBomb)
            {
                whackAMole.LosePoints();
                bomb.SetActive(false);
                //add ux things
            }
            else
            {
                whackAMole.GainPoints();
                //add ux things
            }
            whackAMole.SetHammerWhack(transform.position);
            isUp = false;
            StopCoroutine(ResetAfterTime(0));
            StartCoroutine(ResetAfterTime(0.1f));

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

            if(Random.Range(0f, 1f) < bombChance)
            {
                hasBomb = true;
                bomb.SetActive(true);
            }
            else
            {
                hasBomb = false;
                bomb.SetActive(false);
            }

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
