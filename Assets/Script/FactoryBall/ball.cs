using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public GameObject eff;
    public void destroy(bool isDel) {
        Destroy(this);
        if (isDel) {
            Instantiate(eff, transform.position, transform.rotation);
        }
    }
}
