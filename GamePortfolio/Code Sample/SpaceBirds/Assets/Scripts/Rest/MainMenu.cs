using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider musicSilder;
    [SerializeField] Slider fxSlider;

    public void Play()
    {
        AudioManager.instance.PlaySound(AudioManager.Sound.PlayerShoot);
        SceneManager.LoadScene("GameLevel");
    }

    public void Quit()
    {
        AudioManager.instance.PlaySound(AudioManager.Sound.PlayerShoot);
        Application.Quit();
    }

    public void AdjustFXVolume()
    {
        AudioManager.instance.fxAudioSource.volume = fxSlider.value;
    }
    public void AdjustMusicVolume()
    {
        AudioManager.instance.musicSource.volume = musicSilder.value;
    }


}
