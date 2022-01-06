using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cell : MonoBehaviour
{
    private SpriteRenderer renderer;
    bool isSelect = false;
    Color sel, nsel;
    private void Start() {
        sel = new Color(150, 0, 0);
        nsel = new Color(255,255,255);
        renderer = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown() {
        if (Controller.instance.isPlaying) {
            int col = board_control.instance.getIndexCol(transform.position.x);
            int row = board_control.instance.getIndexRow(transform.position.y);
            board_control.instance.pickCell(col, row);
        }  
    }
    public void Select() {
        isSelect = true;
        renderer.color = sel;
    }
    public void notSelect() {
        isSelect = false;
        renderer.color = nsel;
    }

}
