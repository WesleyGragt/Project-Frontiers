using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private CameraControls cameraControl;
    [SerializeField] private PlayerMovement playerMovement;
    [Header("States")]
    public bool canPause = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu == null) return;
        if (!canPause) return;


        bool isActive = pauseMenu.activeSelf;
        pauseMenu.SetActive(!isActive);

        cameraControl.isLocked = !isActive;
        playerMovement.isLocked = !isActive;

        if (isActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else Cursor.lockState = CursorLockMode.None;

        Time.timeScale = isActive ? 1f : 0f;
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
