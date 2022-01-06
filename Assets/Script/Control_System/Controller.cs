using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    public bool isPlaying, isGameOver;
    public GameObject buttonStart;
    void Start() {
        if (instance == null) instance = this;
        isPlaying = false;
        isGameOver = false;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isGameOver = true;
        }
        if (isGameOver) {
            gameOver();
        }
    }
    public void startPlaying() {
        isPlaying = true;
        isGameOver = false;
        board_control.instance.StartGame();
        Score_Control.instance.StartGame();
        Timmer.instance.resetTime();
    }
    public void gameOver() {
        isPlaying = false;
        Score_Control.instance.saveHighScore();
        queue_Ball.instance.clear();
        buttonStart.SetActive(true);
    }
}
