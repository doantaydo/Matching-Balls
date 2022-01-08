using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Control : MonoBehaviour
{
    public GameObject boomEffect;
    public GameObject[] deleteBallEffect;
    public GameObject moveBallEffect;
    public static Effect_Control instance;
    void Start() {
        if (instance == null) instance = this;    
    }
    public void makeBoomEffect(float col, float row) {
        Instantiate(boomEffect, new Vector3(col, row, 0f), transform.rotation);
    }
    public void makeDeleteBallEffect(int type, float col, float row) {
        if (type == 0 || type == 7 || type == 15) return;
        Instantiate(deleteBallEffect[type], new Vector3(col, row, 0f), transform.rotation);
    }
    public void makeMoveBallEffect(float col, float row) {
        Instantiate(moveBallEffect, new Vector3(col, row, 0f), transform.rotation);
    }
    public void makeMoveGhostBallEffect(float start_col, float start_row, float end_col, float end_row) {
        line_control.instance.setPoint(start_row, start_col, end_row, end_col);
    }
}
