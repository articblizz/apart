using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

    public Vector2 Range;

    float TimeBetween;
    float timer;



	// Use this for initialization
	void Start () {
        //randomIntensity = Random.Range(RandomDif.x , RandomDif.y);
        TimeBetween = Random.Range(Range.x, Range.y);
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= TimeBetween)
        {
            light.enabled = !light.enabled;
            timer = 0;

            TimeBetween = Random.Range(Range.x, Range.y);
        }
	}
}
