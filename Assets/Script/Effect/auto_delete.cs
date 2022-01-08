using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auto_delete : MonoBehaviour
{
    private int TTL;
    public int max_TTL;
    void Start()
    {
        TTL = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (TTL == max_TTL) Destroy(gameObject);
        TTL++;
    }
}
