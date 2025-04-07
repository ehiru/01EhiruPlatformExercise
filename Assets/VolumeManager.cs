using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; // 別忘了引入這個才能用 Slider！

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value; // 打錯成 foat
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        myMixer.SetFloat("vol", Mathf.Log10(volume)*20); // 少了分號
    }
}


// public class VolumeManager : MonoBehaviour
// {
//     public AudioMixer _audio;
//     public GameObject ON;
//     public GameObject OFF;
//     public GameObject PFF;

//    public void SetVolume(float vol)
// {
//     float dB = Mathf.Log10(Mathf.Clamp(vol, 0.0001f, 1f)) * 20;
//     _audio.SetFloat("vol", dB);
// }


//     public void On()
//     {
//         AudioListener.volume = 0;
//         ON.SetActive(false);
//         PFF.SetActive(true);
//     }

//     public void off()
//     {
//         AudioListener.volume = 1;
//         OFF.SetActive(false);
//         ON.SetActive(true);
//     }
// }



