using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line_control : MonoBehaviour
{
    private LineRenderer lr;
    public static line_control instance;
    int count;
    void Start() {
        if (instance == null) instance = this;
        lr = GetComponent<LineRenderer>();
        count = -1;
    }
    void Update() {
        if (count == 0) reset();
        count--;
    }

    public void setPoint(float start_row, float start_col, float end_row, float end_col) {
        lr.SetPosition(0, new Vector3(start_col, start_row, 0f));
        lr.SetPosition(1, new Vector3(end_col, end_row, 0f));
        count = 50;
    }
    void reset() {
        lr.SetPosition(0, new Vector3(0f, 0f, 0f));
        lr.SetPosition(1, new Vector3(0f, 0f, 0f));
    }
}
