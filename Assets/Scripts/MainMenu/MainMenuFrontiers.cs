using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuFrontiers : MonoBehaviour
{



    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    
    }
}
