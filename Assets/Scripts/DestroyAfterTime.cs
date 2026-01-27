using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyDelay;

    private void Start()
    {
        StartCoroutine(DestroySelf(destroyDelay));
    }

    IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
