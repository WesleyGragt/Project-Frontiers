using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private IntVariable coins;
    [SerializeField] private GameObject prize;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (!prize.activeInHierarchy && coins.value == 0)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("EndingBad");
            }
            else if (prize.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("EndingGood");
            }
        }
    }
}