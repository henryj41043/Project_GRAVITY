using UnityEngine;
using System.Collections;

public class AsteroidSpawnerScript : MonoBehaviour
{
    // Component Variables
    Rigidbody body;

    // Gravity Well Object
    GameObject gravityWell;

    // Internal Counter
    public static int asteroidCurCount;
    public static int asteroidMaxCount;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();

        gravityWell = GameObject.Find("Gravity Orb");

        asteroidCurCount = 0;
        asteroidMaxCount = 10;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (asteroidCurCount < asteroidMaxCount)
        {
            asteroidCurCount = asteroidCurCount + 1;
            Instantiate(Resources.Load("Asteroid"), Random.rotation * Vector3.one * GravityWellData.gameWorldRadius, Random.rotation);
        }
	}
}
