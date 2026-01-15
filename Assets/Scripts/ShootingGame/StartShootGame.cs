using UnityEngine;

public class StartShootGame : MonoBehaviour
{
    [SerializeField] private GameObject PlayerGun;

    public void StartGame()
    {
        Debug.Log("Hey");
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        PlayerGun.SetActive(true);
    }
}
