using UnityEngine;

public class ResetCam : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.SetActive(false);
    }
}
