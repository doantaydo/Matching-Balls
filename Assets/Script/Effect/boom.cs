using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    private Vector3 scale;
    void Start()
    {
        scale = new Vector3(0.05f, 0.05f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale += scale;
        if (this.transform.localScale.x > 2f) {
            Destroy(gameObject);
        }
    }
}
