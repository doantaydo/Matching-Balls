using UnityEngine;
using TMPro;

public class Score_Control : MonoBehaviour {
    public static Score_Control instance;

    public TextMeshProUGUI score_txt, highscore_txt;
    public TextMeshProUGUI[] outputfield;
    private int score, highscore;
    string txt_pref;
    
    void Start() {
        if (instance == null) instance = this;
        // print begin score and highscore
        StartGame();
    }
    public void StartGame() {
        score = 0;
        switch (Controller.instance.diff) {
            case 1:
                txt_pref = "highscore_hard";
                break;
            case 2:
                txt_pref = "highscore_nor";
                break;
            case 3:
                txt_pref = "highscore_easy";
                break;
        }

        highscore = PlayerPrefs.GetInt(txt_pref, 0);
        print(score, score_txt);
        print(highscore, highscore_txt);
    }
    public void updateHighScoreTable() {
        print(PlayerPrefs.GetInt("highscore_easy", 0), outputfield[0]);
        print(PlayerPrefs.GetInt("highscore_nor", 0), outputfield[1]);
        print(PlayerPrefs.GetInt("highscore_hard", 0), outputfield[2]);
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
        PlayerPrefs.SetInt(txt_pref,highscore);
    }
    private void print(int score, TextMeshProUGUI txt) {
        // format score to [0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]
        string value = score.ToString();
        while (value.Length != 8) value = "0" + value;
        txt.text = value;
    }
}
