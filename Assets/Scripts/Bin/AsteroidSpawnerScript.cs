using UnityEngine;
using System.Collections;

public class AsteroidSpawnerScript : MonoBehaviour
{
    // Internal Counter
    public static int asteroidCurCount;
    public static int asteroidMaxCount;

    // Use this for initialization
    void Start()
    {
        asteroidCurCount = 0;
        asteroidMaxCount = 50;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (asteroidCurCount < asteroidMaxCount)
        {
            asteroidCurCount = asteroidCurCount + 1;
            Instantiate(Resources.Load("Asteroid"), Random.rotation * Vector3.one * WellDataScript.gameWorldRadius, Random.rotation);
        }
	}
}
