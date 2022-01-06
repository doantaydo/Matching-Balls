using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queue_Ball : MonoBehaviour
{
    public static queue_Ball instance;
    public GameObject[] ballType;
    public GameObject[] place;
    GameObject[] queue_obj;
    int[] queue;
    void Start() {
        if (instance == null) instance = this;
        queue = new int[3];
        queue_obj = new GameObject[3];
        for (int i = 0; i < 3; i++) queue[i] = Random.Range(1,7);
    }

    public int DeQueue() {
        int value = queue[0];
        queue[0] = queue[1];
        queue[1] = queue[2];
        queue[2] = Random.Range(1,ballType.Length + 1);
        return value;
    }
    public void printBall() {
        for (int i = 0; i < 3; i++) {
            if (queue_obj[i] != null) Destroy(queue_obj[i]);
            queue_obj[i] = createBall(queue[i],i);
        }
    }
    public void clear() {
        for (int i = 0; i < 3; i++) {
            if (queue_obj[i] != null) Destroy(queue_obj[i]);
            DeQueue();
            DeQueue();
            DeQueue();
        }
    }
    GameObject createBall(int type, int i) {
        return Instantiate(ballType[type - 1], place[i].transform.position, place[i].transform.rotation);
    }
}
