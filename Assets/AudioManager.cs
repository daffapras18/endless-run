using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------- Audio Clip ---------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip run;
    public AudioClip jump;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void PlayRunSound()
    {
        if (!SFXSource.isPlaying)
        {
            SFXSource.clip = run;
            SFXSource.loop = true;
            SFXSource.Play();
        }
    }

    public void StopRunSound()
    {
        if (SFXSource != null && SFXSource.clip == run && SFXSource.isPlaying)
        {
            SFXSource.Stop();
            SFXSource.loop = false;
        }
    }


    public void PlayJumpSound()
    {
        SFXSource.PlayOneShot(jump);
    }

    public void PlayDeathSound()
    {
        SFXSource.PlayOneShot(death);
    }
}
