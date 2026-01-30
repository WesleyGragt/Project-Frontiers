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
            if (coins.value != 0) return;
            
            if (prize.activeInHierarchy)
            {
                SceneManager.LoadScene("EndingGood");
            }
            else
            {
                SceneManager.LoadScene("EndingBad");
            }
        }
    }
}