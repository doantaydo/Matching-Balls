using UnityEngine;
using TMPro;

public class Timmer : MonoBehaviour
{
    private float hour, minute, second;
    public TextMeshProUGUI time_txt;
    public static Timmer instance;
    void Start() {
        if (instance == null) instance = this;
        resetTime();
    }
    public void resetTime() {
        // reset time to 0:0:0 when start game
        hour = 0;
        minute = 0;
        second = 0;
        time_txt.text = "00:00";
    }
    private void FixedUpdate() {
        if (Controller.instance.isPlaying) {
            // update time
            if (second == 59) {
                second = 0;
                minute++;
                if (minute == 59) {
                    minute = 0;
                    hour++;
                }
            }
            else second++;
            // create string for time in UI
            // sec
            string txt = second.ToString();
            if (second < 10) txt = "0" + txt;
            // minute
            txt = minute.ToString() + ":" + txt;
            if (minute < 10) txt = "0" + txt;
            // hour
            if (hour != 0) txt = hour.ToString() + ":" + txt;
            // print to UI
            time_txt.text = txt;
        }        
    }
}
