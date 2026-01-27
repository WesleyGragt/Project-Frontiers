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
    public GameObject bomb;
    [Range(0f,1f)]
    public float bombChance;
    [Header("UX")]
    public GameObject hitParticle;
    public Transform hitParticleLoc;
    [Space(10)]
    public GameObject explosionParticle;
    public Transform explosionParticleLoc;
    [Space(20)]
    public GameObject hitSound;
    public GameObject explosionSound;
    [Space(20)]
    public Animator animator;
    [Header("States")]
    public bool isUp;
    public bool hasBomb = false;

    public void Whack()
    {
        if (isUp)
        {
            StopAllCoroutines();
            if (hasBomb)
            {
                whackAMole.LosePoints();
                bomb.SetActive(false);

                //ux
                Instantiate(explosionParticle, explosionParticleLoc.position, Quaternion.identity);
                StartCoroutine(ResetAfterTime(digBoomDelay));
            }
            else
            {
                whackAMole.GainPoints();

                //ux
                Instantiate(hitParticle, hitParticleLoc.position, Quaternion.identity);
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
