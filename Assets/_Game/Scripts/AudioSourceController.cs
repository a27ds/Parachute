using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    public AudioClip blip;
    public AudioClip dead;
    public AudioClip end;
    AudioSource audioSource;

    private void OnEnable()
    {
        ParachuterController.Move += PlayBlipAudio;
        ParachuterController.Inwater += PlayDeadAudio;
    }

    private void OnDisable()
    {
        ParachuterController.Move -= PlayBlipAudio;
        ParachuterController.Inwater -= PlayDeadAudio;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeadAudio()
    {
        audioSource.PlayOneShot(dead, 1.0f);
    }

    public void PlayEndAudio()
    {
        audioSource.PlayOneShot(end, 1.0f);
    }

    public void PlayBlipAudio()
    {
        audioSource.PlayOneShot(blip, 1.0f);
    }

}