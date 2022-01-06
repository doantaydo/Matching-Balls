using UnityEngine;
using TMPro;

public class Score_Control : MonoBehaviour {
    public static Score_Control instance;

    public TextMeshProUGUI score_txt, highscore_txt;
    private int score, highscore;
    
    void Start() {
        if (instance == null) instance = this;
        // print begin score and highscore
        StartGame();
    }
    public void StartGame() {
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);
        print(score, score_txt);
        print(highscore, highscore_txt);
    }
    public void getScore(int value) {
        // update score when delete ball
        score += value;
        print(score, score_txt);

        //update highscore if score > highscore
        if (score > highscore) {
            highscore = score;
            print(highscore, highscore_txt);
        }
    }
    public void saveHighScore() {
        PlayerPrefs.SetInt("highscore",highscore);
    }
    private void print(int score, TextMeshProUGUI txt) {
        // format score to [0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]
        string value = score.ToString();
        while (value.Length != 8) value = "0" + value;
        txt.text = value;
    }
}
