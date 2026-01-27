using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer MainAudioMixer;
    [Header("Set ACTIVE when ESC is pressed")]
    [SerializeField] private GameObject[] activateOnEscape;

    [Header("Set INACTIVE when ESC is pressed")]
    [SerializeField] private GameObject[] deactivateOnEscape;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscape();
        }
    }

    void HandleEscape()
    {
        // Deactivate objects
        foreach (GameObject obj in deactivateOnEscape)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // Activate objects
        foreach (GameObject obj in activateOnEscape)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ChangeMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        MainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        MainAudioMixer.SetFloat("SFXVol", sfxVol.value);
    }

}
