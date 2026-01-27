using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private CameraControls cameraControl;
    [SerializeField] private PlayerMovement playerMovement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        if (pauseMenu == null) return;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
