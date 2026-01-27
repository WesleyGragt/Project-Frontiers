using UnityEngine;

public class Resize : MonoBehaviour
{
    [SerializeField] float scaleFactor = 1.0f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void sizeUp()
    {
        transform.localScale *= scaleFactor;
    }

    public void resetSize()
    {
        transform.localScale = originalScale;
    }
}
