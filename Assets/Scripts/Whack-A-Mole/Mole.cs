using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public WhackAMole whackAMole;
    public Transform topLocation;
    public Transform bottomLocation;
    public float digHitDelay;
    public float digBoomDelay;
    public LerpTo lerp;
    public GameObject[] moles;
    public GameObject candyMole;
    [Range(0f,1f)]
    public float candyPopupTimeMult;
    public GameObject bombMole;
    [Range(0f,1f)]
    public float bombChance;
    [Range(0f, 1f)]
    public float candyChance;
    [Header("UX")]
    public GameObject hitParticle;
    public Transform hitParticleLoc;
    [Space(10)]
    public GameObject explosionParticle;
    public Transform explosionParticleLoc;
    [Space(20)]
    public GameObject hitSound;
    public GameObject explosionSound;
    public GameObject ouchSound;
    public GameObject laughSound;
    [Space(20)]
    public Animator animator;
    [Header("States")]
    public bool isUp;
    public bool hasBomb = false;
    public bool hasCandy = false;

    public void Whack()
    {
        if (isUp)
        {
            StopAllCoroutines();
            if (hasBomb)
            {
                whackAMole.LosePoints();

                //ux
                Instantiate(explosionParticle, explosionParticleLoc.position, Quaternion.identity);
                Instantiate(explosionSound, explosionParticleLoc.position, Quaternion.identity);
                Instantiate(laughSound, bombMole.transform.position, Quaternion.identity);
                Instantiate(hitSound, hitParticleLoc.position, Quaternion.identity);
                StartCoroutine(ResetAfterTime(digBoomDelay));
            }
            else
            {
                if(hasCandy)
                {
                    whackAMole.GainDoublePoints();
                }
                else
                {
                    whackAMole.GainPoints();
                }

                //ux
                Instantiate(hitParticle, hitParticleLoc.position, Quaternion.identity);
                Instantiate(hitSound, hitParticleLoc.position, Quaternion.identity);
                Instantiate(ouchSound, moles[0].transform.position, Quaternion.identity);
                animator.SetTrigger("Hit");
                StartCoroutine(ResetAfterTime(digHitDelay));
            }
            whackAMole.SetHammerWhack(transform.position);
            isUp = false;

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
            hasBomb = false;
            hasCandy = false;

            //turn off all moles
            for (int i = 0; i < moles.Length; i++)
            {
                moles[i].SetActive(false);
            }
            candyMole.SetActive(false);
            bombMole.SetActive(false);

            if(Random.Range(0f, 1f) < bombChance)
            {
                hasBomb = true;
                bombMole.SetActive(true);
                StartCoroutine(ResetAfterTime(time));
            }
            else if(Random.Range(0f, 1f) < candyChance)
            {
                hasCandy = true;
                candyMole.SetActive(true);
                StartCoroutine(ResetAfterTime(time * candyPopupTimeMult));
            }
            else
            {
                moles[Random.Range(0, moles.Length)].SetActive(true);
                StartCoroutine(ResetAfterTime(time));
            }

            lerp.target = topLocation;
        }
    }

    IEnumerator ResetAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Reset();
    }
}
