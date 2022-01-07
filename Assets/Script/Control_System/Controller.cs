using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    public bool isPlaying, isGameOver;
    public GameObject[] button;
    public Slider diffSlider;
    public int diff;

    void Start() {
        if (instance == null) instance = this;

        LoadDiff();
        changeDiff();

        isPlaying = false;
        isGameOver = false;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isGameOver = true;
        }
        if (isGameOver && isPlaying) {
            gameOver();
        }
    }
    /*
        SET DIFF
    */
    public void changeDiff() {
        float value = diffSlider.value;
        if (value < 0.35) setEasy();
        else if (value < 0.7) setNormal();
        else setHard();
        SaveDiff();
    }
    public void SaveDiff() {
        PlayerPrefs.SetFloat("diff", diffSlider.value);
    }
    public void LoadDiff() {
        diffSlider.value = PlayerPrefs.GetFloat("diff", 0);
    }
    public void setEasy() {
        diff = 3;
    }
    public void setNormal() {
        diff = 2;
    }
    public void setHard() {
        diff = 1;
    }
    public void startPlaying() {
        isPlaying = true;
        isGameOver = false;

        board_control.instance.StartGame();
        Score_Control.instance.StartGame();
        Timmer.instance.resetTime();
        Sound_Control.instance.StartSound();

        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(false);
    }
    public void gameOver() {
        isPlaying = false;

        Score_Control.instance.saveHighScore();
        queue_Ball.instance.clear();
        Sound_Control.instance.gameOver();

        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(true);
    }
}
