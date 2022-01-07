using UnityEngine;
using UnityEngine.UI;

public class Sound_Control : MonoBehaviour
{
    public static Sound_Control instance;
    public AudioSource pickSound, moveSound, delete_ballSound, boomSound, backgroundSound, gameOverSound;
    [SerializeField] Slider volumeSlider;
    void Start()
    {
        if (instance == null) instance = this;
        Load();
        ChangeVolume();
        StartSound();
    }
    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
    }
    private void Save() {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
    public void pick() {
        pickSound.Play();
    }
    public void move() {
        moveSound.Play();
    }
    public void deleteBall() {
        delete_ballSound.Play();
    }
    public void boom() {
        boomSound.Play();
    }
    public void StartSound() {
        if (!backgroundSound.isPlaying) backgroundSound.Play();
    }
    public void gameOver() {
        backgroundSound.Stop();
        gameOverSound.Play();
    }

}
