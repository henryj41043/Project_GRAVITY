using UnityEngine;
using System.Collections;

public class ShipSpawnerScript : MonoBehaviour
{
    // Internal Counter
    public static int shipCurCount;
    static int shipMaxCount;
    static float timer;

    // Use this for initialization
    void Start()
    {
        shipCurCount = 0;
        shipMaxCount = 19;
        timer = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ((shipCurCount < shipMaxCount) && (timer <= 0.0f))
        {
            timer = 1.0f;
            shipCurCount = shipCurCount + 1;
            Rigidbody enemy = ((GameObject)Instantiate(Resources.Load("Enemy"), Random.rotation * Vector3.forward * (WellDataScript.gameWorldRadius - 1.0f), Random.rotation)).GetComponent<Rigidbody>();
            enemy.velocity = Quaternion.LookRotation(GameObject.Find("Gravity Orb").transform.position - enemy.transform.position) * Vector3.forward * 100.0f;
        }
        else
        {
            timer = timer - Time.deltaTime;
        }
	}
}
