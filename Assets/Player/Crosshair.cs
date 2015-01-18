using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crosshair : MonoBehaviour {

	public Texture2D[] Textures;
	
	public GUIStyle gStyle;

	
	Rect position1;
	Rect position2;

	bool lookingAtObject = false;
	public bool Enabled = true;

	// Use this for initialization
	void Start () {
	
		position1 = new Rect((Screen.width - Textures[0].width) /2, (Screen.height - Textures[0].height)/2, 
		                    Textures[0].width, Textures[0].height);
		position2 = new Rect((Screen.width - Textures[1].width) /2, (Screen.height - Textures[1].height)/2, 
		                    Textures[1].width, Textures[1].height);
	}

	void Update () {

		RaycastHit hit;
		Ray raycast = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
		Debug.DrawRay(raycast.origin, raycast.direction * 3, Color.red);
		
		if(Physics.Raycast(raycast, out hit, 3))
		{
			if(hit.collider.gameObject.tag == "Item" || hit.collider.gameObject.tag == "Battery")
				lookingAtObject = true;
			else
				lookingAtObject = false;
		}
		else
			lookingAtObject = false;
		
	}

	void OnGUI()
	{
		if(Enabled)
		{
			if(lookingAtObject)
			{
				GUI.DrawTexture(position2, Textures[1]);
				//GUI.Label(new Rect(position.x + 20,position.y - 10, 100, 25),"PICK UP", gStyle );
			}
			else
				GUI.DrawTexture(position1, Textures[0]);
		}
		
	}
}
