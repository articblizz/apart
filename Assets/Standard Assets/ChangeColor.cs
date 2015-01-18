using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

	public GameObject Map;

	// Use this for initialization
	void Start () {

		Map.renderer.material.color = Color.green * 0.1f;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
