using UnityEngine;
using System.Collections;

public class ShipDestroyScript : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision Detected!");
        if (col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
            Application.LoadLevel(0);
        }
        if (col.gameObject.name == "Asteroid(Clone)")
        {
            GameObject explosion = Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity) as GameObject;
            Destroy(col.gameObject);
            AsteroidSpawnerScript.asteroidCurCount = AsteroidSpawnerScript.asteroidCurCount - 1;
        }
        if (col.gameObject.name == "Enemy(Clone)")
        {
            GameObject explosion = Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.identity) as GameObject;
            Destroy(col.gameObject);
            ShipSpawnerScript.shipCurCount = ShipSpawnerScript.shipCurCount - 1;
        }
    }
}
