using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        instance = this;

        // Optional fallback in case sources aren’t assigned in inspector
        if (sfxSource == null) sfxSource = GetComponent<AudioSource>();
        if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        sfxSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        musicSource.clip = musicClip;
        musicSource.loop = loop;
        musicSource.Play();
    }
}



// using UnityEngine;

// public class SoundManager : MonoBehaviour
// {
//     public static SoundManager instance;

//     private void Awake()
//     {
//         if (instance == null)
//             instance = this;
//         else
//             Destroy(gameObject);
//     }

//     public void PlaySound(AudioClip soundName)
//     {

//     }

//     public void ChangeSoundVolume(float soundIncrement)
//     {

//     }

//     public void ChangeMusicVolume(float musicIncrement)
//     {

//     }
// }
