using UnityEngine;

public class StartShootGame : MonoBehaviour
{
    [SerializeField] private GameObject PlayerGun;

    public void StartGame()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        PlayerGun.SetActive(true);
    }
}
