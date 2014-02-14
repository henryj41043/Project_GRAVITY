using UnityEngine;
using System.Collections;

public class AsteroidSpawnerScript : MonoBehaviour
{
    // Internal Counter
    public static int asteroidCurCount;
    public static int asteroidMaxCount;
    static float timer;

    // Use this for initialization
    void Start()
    {
        asteroidCurCount = 0;
        asteroidMaxCount = 50;
        timer = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ((asteroidCurCount < asteroidMaxCount) && (timer <= 0.0f))
        {
            timer = 0.2f;
            asteroidCurCount = asteroidCurCount + 1;
            Rigidbody asteroid = ((GameObject)Instantiate(Resources.Load("Asteroid"), Random.rotation * Vector3.forward * (WellDataScript.gameWorldRadius - 1.0f), Random.rotation)).GetComponent<Rigidbody>();
            asteroid.velocity = Quaternion.LookRotation(GameObject.Find("Gravity Orb").transform.position - asteroid.transform.position) *Vector3.forward * Random.RandomRange(20.0f, 200.0f);
        }
        else
        {
            timer = timer - Time.deltaTime;
        }
	}
}
