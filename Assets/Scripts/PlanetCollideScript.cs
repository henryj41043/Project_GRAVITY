using UnityEngine;
using System.Collections;

public class PlanetCollideScript : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Detected!");
        if (col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
            Application.LoadLevel(0);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger Detected!");
        if (col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
            Application.LoadLevel(0);
        }
    }
}
