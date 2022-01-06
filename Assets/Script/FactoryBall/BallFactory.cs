using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public GameObject[] type_Ball;
    public static BallFactory instance;
    void Start()
    {
        if (instance == null) instance = this;
    }
    public GameObject createBall(int type, GameObject pos) {
        GameObject prefab = type_Ball[type];
        return Instantiate(prefab, pos.transform.position, pos.transform.rotation);
    }
}
