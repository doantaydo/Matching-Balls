using UnityEngine;

public class auto_delete : MonoBehaviour
{
    void Start()
    {
        DestroyObjectDelayed();
    }
    void DestroyObjectDelayed()
    {
        // Kills the game object in 0.5 seconds after loading the object
        Destroy(gameObject, 0.5f);
    }
}
