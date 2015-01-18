using UnityEngine;
using System.Collections;

public class BatterySpawner : MonoBehaviour {

	public GameObject Battery;

	// Use this for initialization
	void Start () {
	
	}


	float timer;
	// Update is called once per frame
	void Update () {
		timer++;

		if (timer == 2) 
		{
			Instantiate (Battery, this.transform.position, this.transform.rotation);
			timer = 0;
		}

	}
}
