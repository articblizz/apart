using UnityEngine;
using System.Collections;

public class FlashlightOnCamera : MonoBehaviour {

	public GameObject FlashLight;

	public float Battery = 100;
	public float BatteryLossPerTick = 0.003f;
	public float BatteryRechargeRate = 0.02f;
	public float BatteryStartsFlickeringAt = 6;
	float randomIntensity = 0.0f;

	public AudioClip FlashlightOff;
	public AudioClip FlashlightOn;

	public int Batterys;
	public float ChargePerBattery;
	
	// Use this for initialization
	void Start () 
	{
		randomIntensity = Random.Range(0.1f, 1.6f);
	}

	void AddBattery()
	{
		Batterys++;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(FlashLight.light.enabled)
		{
			if(Battery > 0)
				Battery -= BatteryLossPerTick;
			else if(Battery <= 0)
			{
				Battery = 0;
				FlashLight.light.enabled = false;
			}

			if(Battery < BatteryStartsFlickeringAt)
			{

				if(Mathf.Clamp(randomIntensity,0.1f,1.5f) == FlashLight.light.intensity )
				{
					randomIntensity = Random.Range(0.1f,1.5f);
				}
				float change = Random.Range(10,20);
				if(randomIntensity < FlashLight.light.intensity)
				{
					FlashLight.light.intensity -= change * Time.deltaTime;
				}
				else if(randomIntensity > FlashLight.light.intensity)
				{
					FlashLight.light.intensity += change * Time.deltaTime;
				}
			}
			else
			{
				FlashLight.light.intensity = 1.5f;
			}
		}
		else if(Battery < 100)
		{
			Battery += BatteryRechargeRate;
			if(Battery > 100)
				Battery = 100;
		}

		FlashLight.transform.parent = Camera.main.transform;

		if(Input.GetMouseButtonDown(1))
		{
			if(FlashLight.light.enabled)
			{
				FlashLight.light.enabled = false;
				audio.clip = FlashlightOff;
			}
			else
			{
				FlashLight.light.enabled = true;
				audio.clip = FlashlightOn;
			}
			audio.Play();
			
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			if(Batterys > 0)
			{
				Batterys--;
				Battery += ChargePerBattery;
				if(Battery > 100)
					Battery = 100;
			}
		}
	}

	void OnGUI()
	{
		UnityEngine.GUI.Label(new Rect(10,10,60,25), string.Format("{0:0.0} : {1}",Battery, Batterys));
	}
}
