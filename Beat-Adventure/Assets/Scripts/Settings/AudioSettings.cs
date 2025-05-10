using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioSource musicSource;
    public AudioSource[] sfxSources;

    void Start()
    {
        // Cargar preferencias guardadas
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        ApplyVolumes();
    }

    public void ApplyVolumes()
    {
        float master = masterSlider.value;
        float music = musicSlider.value * master;
        float sfx = sfxSlider.value * master;

        AudioListener.volume = master;
        if (musicSource != null) musicSource.volume = music;

        foreach (var source in sfxSources)
        {
            if (source != null) source.volume = sfx;
        }

        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

}
