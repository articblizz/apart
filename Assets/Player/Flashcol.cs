using UnityEngine;
using System.Collections;

public class Flashcol : MonoBehaviour {

	GameObject monster;
	public GameObject Player;
	public float BatteryLossPerTick = 0.2f;

	public Light myFlashlight;

	// Use this for initialization
	void Start () {
		monster = GameObject.FindGameObjectsWithTag("Monster")[0];
	
	}

	void DrainMonster()
	{
		if (monster != null)
		{
			monster.SendMessage("DrainHp", 0.2f);
			Player.GetComponent<FlashlightOnCamera>().Battery -= BatteryLossPerTick;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if(myFlashlight.enabled)
		{
			if(monster != null)
			{
				if(collider.bounds.Contains(monster.transform.position))
				{
					DrainMonster();
				}
			}
		}
	}

	void OnTriggerStay(Collider col)
	{
		if(myFlashlight.enabled)
		{
			if(col.gameObject.tag == "Monster")
				DrainMonster();
		}
	}
}
