using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;
    

    public void PopSoundEffect()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    public void MetalSoundEffect()
    {
        audioSource.clip = clips[1];
        audioSource.Play();
    }

    public void ClickSoundEffect()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    public void ExtraSoundEffect()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
    }
}
