using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public CameraControls cameraControls;
    Camera camera;
    public Transform cameraLockTarget;
    public PauseMenu pauseMenu;

    private void Start()
    {
        if(cameraControls == null)
        {
            cameraControls = FindFirstObjectByType<CameraControls>();
        }
    }

    public void LockCamera()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.canPause = false;
        camera = cameraControls.camera;
        LerpTo camLerp = camera.GetComponent<LerpTo>();
        cameraControls.isLocked = true;
        cameraControls.player.isLocked = true;
        camLerp.target = cameraLockTarget;
        camLerp.isLerping = true;
        camLerp.isRotateLerping = true;
    }

    public void UnlockCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.canPause = true;
        LerpTo camLerp = camera.GetComponent<LerpTo>();
        cameraControls.isLocked = false;
        cameraControls.player.isLocked = false;
        camLerp.target = cameraControls.transform;
        camLerp.isLerping = true;
        camLerp.isRotateLerping = true;
    }
}
