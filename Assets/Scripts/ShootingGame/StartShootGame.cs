using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class StartShootGame : MonoBehaviour
{
    [SerializeField] private GameObject PlayerGun;

    public void StartGame()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        PlayerGun.SetActive(true);
    }
}
