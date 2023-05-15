using UnityEngine.Audio;
using UnityEngine;

public class DiscSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

   public void PlayAudio()
    {
        source.PlayOneShot(clip);
    }
}
