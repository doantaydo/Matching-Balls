using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    public bool isPlaying, isGameOver;
    public GameObject[] button;
    public int diff;
    void Start() {
        diff = 3;
        if (instance == null) instance = this;
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
        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(false);
    }
    public void gameOver() {
        isPlaying = false;
        Score_Control.instance.saveHighScore();
        queue_Ball.instance.clear();
        for (int i = 0; i < button.Length; i++)
            button[i].SetActive(true);
    }
}
