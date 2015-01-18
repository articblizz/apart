using UnityEngine;
using System.Collections;

public class MapSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if(!Debug.isDebugBuild)
		{
			Screen.showCursor = false;
			Screen.lockCursor = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
