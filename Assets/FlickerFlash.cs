using UnityEngine;
using System.Collections;

public enum Wave_Function
{
	SIN,
	NOISE,
};
public class FlickerFlash : MonoBehaviour
{

	public Wave_Function waveFunction = Wave_Function.NOISE;//Använd NOISE för Flicker effekten
	public float baseValue = 0.0f; // 
	public float amplitude = 1.0f; // amplituden är vågens storlek
	public float phase = 0.0f; // start punkten i vågen
	public float frequency = 0.5f; // frekvensen på vågen, alltå hur "täta" vågorna är
	Light light;

	private Color originalColor;
	private bool isActive = true;
	void Start ()
	{
		light = GetComponent<Light> ();
		originalColor = light.color;
	}
	
	void Update () 
	{
		if( isActive == true )
			light.color = originalColor * (EvaluateWave());
	}
	
	float EvaluateWave () 
	{
		float x = (Time.time + phase)*frequency;
		float y = 0;
		
		x = x - Mathf.Floor(x); // Normalisera värdet mellan 0-1
		 

		switch (waveFunction)
		{
			case Wave_Function.NOISE:
			y = 1 - (Random.value * 2.0f);
			break;

			case Wave_Function.SIN:
			y = Mathf.Sin(x*2.0f*Mathf.PI);
			break;

		}
		return (y*amplitude)+baseValue;

	}

	public void StartFlicker()
	{
		isActive = true;
	}

	public void StopFlicker()
	{
		isActive = false;
		light.color = originalColor;
	}
}
