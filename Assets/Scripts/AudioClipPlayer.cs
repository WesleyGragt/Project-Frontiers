using UnityEngine;
using UnityEngine.Audio;

public class AudioClipPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public float randomPitchChange;

    private void Start()
    {
        audioSource.pitch += Random.Range(-randomPitchChange, randomPitchChange);
    }
}
