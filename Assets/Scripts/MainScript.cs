using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer musicMixer;
    public Slider volumeSlider;
    public Slider musicSlider;
    // Start is called before the first frame update

    public GameObject menuLeave;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuLeave.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.45f);
            volumeSlider.value = 0.45f;
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
            SetVolume(PlayerPrefs.GetFloat("volume"));
        }

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 0.45f);
            musicSlider.value = 0.45f;
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("music");
            SetVolume(PlayerPrefs.GetFloat("music"));
        }
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        float gamevol = Mathf.Lerp(-40, 10, volume);
        if (volume == 0)
        {
            gamevol = -80;
        }
        audioMixer.SetFloat("volume", gamevol);
    }
    public void SetMusic(float volume)
    {
        PlayerPrefs.SetFloat("music", volume);
        float gamevol = Mathf.Lerp(-40, 10, volume);
        if (volume == 0)
        {
            gamevol = -80;
        }
        musicMixer.SetFloat("music", gamevol);
    }



}
