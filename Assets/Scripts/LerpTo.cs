using UnityEngine;

public class LerpTo : MonoBehaviour
{
    public float lerpSpeed;
    public bool isLerping;
    public float rotateLerpSpeed;
    public bool isRotateLerping;
    public Transform target;

    private void Update()
    {
        if (isLerping)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.deltaTime);
        }
        if (isRotateLerping)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotateLerpSpeed * Time.deltaTime);
        }
    }
}
