using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board_control : MonoBehaviour
{
    public static board_control instance;
    public GameObject[] cell_type;
    GameObject[,] board_obj;
    int[,] board_data;
    bool isFirstGame, isPicked, isUpdate;
    int row_1, col_1;

    void Start() {
        if (instance == null) instance = this;

        board_obj = new GameObject[9,9];
        board_data = new int[9,9];

        isFirstGame = true;
        isPicked = false;
        isUpdate = false;

        resetBoard();
    }
    /*
        FUNCTION TO START GAME
    */
    public void StartGame(){
        if (!isFirstGame) {
            resetBoard();
        }
        else isFirstGame = false;
        randomBoard();
        queue_Ball.instance.printBall();
    }
    void getQueueBall() {
        int numBall = 0;
        int[,] list_cell_empty = new int[81, 2];
        int size = 0;
        for (int row = 0; row < 9; row++)
            for (int col = 0; col < 9; col++)
                if (board_data[row, col] == 0) {
                    list_cell_empty[size,0] = row;
                    list_cell_empty[size,1] = col;
                    size++;
                }

        if (size >= 3) {
            int count = 0;
            while (count < 3) {
                int value = queue_Ball.instance.DeQueue();
                size--;
                int index = Random.Range(0, size);

                updateBoard(list_cell_empty[index, 0], list_cell_empty[index, 1], value);
                
                while(index < size) {
                    list_cell_empty[index, 0] = list_cell_empty[index + 1, 0];
                    list_cell_empty[index, 1] = list_cell_empty[index + 1, 1];
                    index++;
                }

                list_cell_empty[index, 0] = -1;
                count++;
            }
        }
        else if (size == 2) {
            int value1 = queue_Ball.instance.DeQueue();
            int value2 = queue_Ball.instance.DeQueue();
            if (Random.Range(1,2) == 1) {
                updateBoard(list_cell_empty[0, 0], list_cell_empty[0, 1], value1);
                updateBoard(list_cell_empty[1, 0], list_cell_empty[1, 1], value2);
            }
            else {
                updateBoard(list_cell_empty[0, 0], list_cell_empty[0, 1], value2);
                updateBoard(list_cell_empty[1, 0], list_cell_empty[1, 1], value1);
            }
        }
        else if (size == 1) {
            int value = queue_Ball.instance.DeQueue();
            updateBoard(list_cell_empty[0,0], list_cell_empty[0,1], value);
        }
        queue_Ball.instance.printBall();
        checkMatching();
        if (checkFull()) {
            Debug.Log("GameOver");
            Controller.instance.isGameOver = true;
        }
    }
    bool checkFull() {
        for (int row = 0; row < 9; row++)
            for (int col = 0; col < 9; col++)
                if (board_data[row, col] == 0) return false;
        return true;
    }
    /*
        FIND ALL MATCHING PLACE AND DELETE, ADD SCORE
    */
    void checkMatching() {
        bool useBoom = false;
        int[,] list_delete = new int[81,2];
        bool loop = false;
        int size = 0;
        for (int row = 0; row < 9; row++) {
            for (int col = 0; col < 9; col++) {
                int type = board_data[row, col];
                if (type == 15) {
                    useBoom = true;
                // if match ball after move
                    for (int i = -1; i < 2; i++)
                        for (int j = -1; j < 2; j++)
                            if (checkValue(row + i) && checkValue(col + j)) {
                                if (board_data[row + i, col + j] == 7) {
                                    // if boom match another boom --> loop again to delete new boom
                                    board_data[row + i, col + j] = 15;
                                    loop = true;
                                }
                                else if (board_data[row + i, col + j] != 0) {
                                    if (!haveInList(list_delete, row + i, col + j, size)) {
                                        list_delete[size, 0] = row + i;
                                        list_delete[size, 1] = col + j;
                                        size++;
                                    }
                                }
                            }
                }
                else if (type != 0 && type != 7) {
                    // find same balls
                    for (int next_row = -1; next_row < 2; next_row++) {
                        for (int next_col = -1; next_col < 2; next_col++) {
                            if (!(next_row == 0 && next_col == 0)) {
                                int max = findNext(row, col, next_row, next_col, 1);
                                if (max >= 5) {
                                    Debug.Log("Find: " + row + " " + col + " : " + type + " : " + max);
                                    int now_row = row;
                                    int now_col = col;
                                    while(max != 0) {
                                        if(!haveInList(list_delete, now_row, now_col, size)) {
                                            list_delete[size, 0] = now_row;
                                            list_delete[size, 1] = now_col;
                                            size++;
                                        }
                                        now_row += next_row;
                                        now_col += next_col;
                                        max--;
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
        }
        if(size > 0) {
            if (useBoom) Sound_Control.instance.boom();
            else Sound_Control.instance.deleteBall();
            waitting(1);
        }
        for (int i = 0; i < size; i++) {
            updateBoard(list_delete[i,0], list_delete[i,1], 0);
        }
        Score_Control.instance.getScore(size);
        if (loop) {
            checkMatching();
        }
    }
    bool haveInList(int[,] list_delete, int row, int col, int size) {
        for (int i = 0; i < size; i++) {
            if (list_delete[i, 0] == row && list_delete[i, 1] == col) return true;
        }
        return false;
    }
    int findNext(int row, int col, int next_row, int next_col, int count) {
        if (checkValue(row + next_row) && checkValue(col + next_col)) {
            int type = board_data[row, col];
            if (board_data[row + next_row, col + next_col] == type)
                return findNext(row + next_row, col + next_col, next_row, next_col, count + 1);
        }
        return count;
    }
    /*
        RANDOM 3 BALLS WHEN START GAME
    */
    void randomBoard() {
        for (int i = 0; i < 3; i++) {
            int row = Random.Range(0,8);
            int col = Random.Range(0,8);
            int type = Random.Range(Controller.instance.diff,7);
            while(board_data[row, col] != 0) {
                row = Random.Range(0, 8);
                col = Random.Range(0, 8);
            }
            updateBoard(row, col, type);
        }
    } 
    /*
        CELL FACTORY
    */
    GameObject createCell(int type, float col, float row) {
        return Instantiate(cell_type[type], new Vector3(col, row, 0f), transform.rotation);
    }

    /*
        FUNCTION TO EDIT BOARD VALUE
    */
    void resetBoard() {
        for (int row = 0; row < 9; row++)
            for (int col = 0; col < 9; col++)
                updateBoard(row, col, 0);
    }
    void updateBoard(int row, int col, int type) {
        if (!checkValue(row) || !checkValue(col)) {
            Debug.Log(row + " " + col);
            return;
        }
        board_data[row, col] = type;

        if (board_obj[row, col]) Destroy(board_obj[row, col]);
        board_obj[row, col] = createCell(type, getCol(col), getRow(row));
    }
    /*
        PICK A BALL AND GO TO ANOTHER
    */
    public void pickCell(int col, int row) {
        if (isUpdate) return;
        if (isPicked) {
            if (board_data[row, col] != 0) {
                Sound_Control.instance.pick();
                board_obj[row_1, col_1].GetComponent<cell>().notSelect();
                row_1 = row;
                col_1 = col;
                board_obj[row, col].GetComponent<cell>().Select();
            }
            else {
                if (row == row_1 && col == col_1) return;

                if (checkCanMove(row_1, col_1, row, col)) {
                    updateBoard(row, col, board_data[row_1, col_1]);
                    updateBoard(row_1, col_1, 0);
                    if (board_data[row, col] == 7) board_data[row, col] = 15;
                    Sound_Control.instance.move();
                    checkMatching();
                    waitting(1);
                    getQueueBall();
                    // delete list ball if match after
                    checkMatching();
                    isUpdate = false;
                }
                else board_obj[row_1, col_1].GetComponent<cell>().notSelect();
                
                isPicked = false;
            }
        }
        else {
            if (board_data[row, col] != 0) {
                Sound_Control.instance.pick();
                isPicked = true;
                row_1 = row;
                col_1 = col;
                board_obj[row, col].GetComponent<cell>().Select(); 
            }
        }
    }
    /*
        WAITTING FUNCTION
    */
    void waitting(int sec) {
        return;
        // WAITTING ...
        float start_time = Time.time;
        Debug.Log("Start: " + start_time);
        while(true) {
            float time = Time.time;
            if (sec < time - start_time) {
                Debug.Log("End: " + time);
                break;
            }
        }
    }
    /*
        CHECK CAN MOVE WITH BFS
    */
    bool checkCanMove(int start_row, int start_col, int end_row, int end_col) {
        if (board_data[start_row, start_col] == 8) return true;
        if (start_col == end_col && start_row == end_row)
            return true;

        int[,] visited = new int[81,2];
        for (int i = 0; i < 81; i++) {
            visited[i,0] = -1;
            visited[i,1] = -1;
        }

        visited[0,0] = start_row;
        visited[0,1] = start_col;

        int row, col;
        int count = 0;
        int size = 1;

        while(count < size && count < 80) {
            row = visited[count,0];
            col = visited[count,1];
            count++;
            if (row == -1) break;
            if (row == end_row && col == end_col) return true;
            int off_col, off_row;
            // 1 0
            off_row = 1;
            off_col = 0;
            if (checkEmpty(row + off_row, col + off_col))
                if (!find_visited(visited, row + off_row, col + off_col)) {
                    if (row + off_row == end_row && col + off_col == end_col) return true;
                    visited[size,0] = row + off_row;
                    visited[size,1] = col + off_col;
                    size++;
                }
            // 0 1
            off_row = 0;
            off_col = 1;
            if (checkEmpty(row + off_row, col + off_col))
                if (!find_visited(visited, row + off_row, col + off_col)) {
                    if (row + off_row == end_row && col + off_col == end_col) return true;
                    visited[size,0] = row + off_row;
                    visited[size,1] = col + off_col;
                    size++;
                }
            // -1 0
            off_row = -1;
            off_col = 0;
            if (checkEmpty(row + off_row, col + off_col))
                if (!find_visited(visited, row + off_row, col + off_col)) {
                    if (row + off_row == end_row && col + off_col == end_col) return true;
                    visited[size,0] = row + off_row;
                    visited[size,1] = col + off_col;
                    size++;
                }
            // 0 -1
            off_row = 0;
            off_col = -1;
            if (checkEmpty(row + off_row, col + off_col))
                if (!find_visited(visited, row + off_row, col + off_col)) {
                    if (row + off_row == end_row && col + off_col == end_col) return true;
                    visited[size,0] = row + off_row;
                    visited[size,1] = col + off_col;
                    size++;
                } 
        }
        if (count > 81) Debug.Log("infinity loop");
        Debug.Log("CANNOT" + size);
        return false;
    }
    bool checkEmpty(int row, int col) {
        if (checkValue(row) && checkValue(col))
            if (board_data[row, col] == 0) return true;
        return false;
    }
    bool find_visited(int[,] arr, int row, int col) {
        for (int i = 0; i < 81; i++) {
            if (arr[i,0] == -1) return false;
            if (arr[i,0] == row && arr[i,1] == col) return true;
        }
        return false;
    }
    bool checkValue(int pos) {
        return !(pos < 0 || pos > 8);
    }
    /*
        FUNCTION TO GET INDEX AND POSITION OF ROW AND COL
    */
    public int getIndexRow(float row) {
        return (int)((2.73f - row)/0.9075f);
    }
    public int getIndexCol(float col) {
        return (int)((3.63f + col)/0.9075f);
    }
    public float getCol(int i) {
        return -3.63f + i * 0.9075f;
    }
    public float getRow(int i) {
        return 2.73f - i * 0.9075f;
    }
}
