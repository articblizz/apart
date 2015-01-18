using UnityEngine;
using System.Collections;

public class PreventItemsUnderground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		var pos = transform.position;
		if(pos.y < -10)
		{
			rigidbody.MovePosition(new Vector3(pos.x, 10, pos.z));
		}
	}
}
