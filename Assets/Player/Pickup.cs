using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {


	public float[] ItemSpeedReduction;

	public float PickupLength = 1;

	public int Batterys;
	public int ChargePerBattery;

	float sizeChange = 4;


	GameObject isPickedUp;
	Collider pickedUpCollider;

	public LayerMask myLayerMask;

	int oldLayer = 0;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate()
	{
		var pos = Camera.main.transform;
		
		if(isPickedUp)
		{
			isPickedUp.rigidbody.velocity = Vector3.zero;
			isPickedUp.rigidbody.MoveRotation(pos.rotation);
			isPickedUp.rigidbody.MovePosition(pos.position - (pos.up * 0.5f) + pos.forward - pos.right);
		}
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		Ray raycast = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

		if(Physics.Raycast(raycast, out hit, PickupLength, myLayerMask) && !isPickedUp)
		{
			if(hit.collider.gameObject.tag == "Item")
			{
				if(Input.GetMouseButtonDown(0))
				{
					if(isPickedUp)
						return;

					pickedUpCollider = hit.collider;
					if(hit.collider.transform.parent != null)
						isPickedUp = hit.collider.transform.parent.gameObject;
					else
						isPickedUp = hit.collider.gameObject;

					pickedUpCollider.enabled = false;

					// sätter objektet i GUI layer
					SetLayerRecursively(isPickedUp, 11);
					isPickedUp.layer = 11;

					isPickedUp.transform.localScale /= sizeChange;
					ReduceMovementspeed(isPickedUp.name);
					
					isPickedUp.transform.parent = Camera.main.transform;
					isPickedUp.rigidbody.useGravity = false;
				}
			}
			else if(hit.collider.gameObject.tag == "Battery")
			{
				if(Input.GetMouseButtonDown(0))
				{
					Destroy(hit.collider.gameObject);
					
					gameObject.SendMessage("AddBattery");
				}
			}
		}
		else if(Input.GetMouseButtonDown(0) && isPickedUp)
		{
			isPickedUp.rigidbody.useGravity = true;
			SetLayerRecursively(isPickedUp, oldLayer);
		
			pickedUpCollider.enabled = true;

			gameObject.SendMessage("ResetSpeed");
			isPickedUp.transform.localScale *= sizeChange;

			isPickedUp.transform.parent = null;
			isPickedUp = null;
		}
	}

	void SetLayerRecursively(GameObject obj, int newLayer)
	{
		if (null == obj)
		{
			return;
		}
		oldLayer = obj.layer;
		
		obj.layer = newLayer;
		
		foreach (Transform child in obj.transform)
		{
			if (null == child)
			{
				continue;
			}
			SetLayerRecursively(child.gameObject, newLayer);
		}
	}

	void ReduceMovementspeed(string objName)
	{
		switch(objName)
		{
		case "Exhaust pipe":
			Reduce(ItemSpeedReduction[0]);
			break;
		case "Transmission Box":
			Reduce(ItemSpeedReduction[1]);
			break;

		case "Tire":
			Reduce(ItemSpeedReduction[2]);
			break;
		}
	}

	void Reduce(float percent)
	{
		gameObject.SendMessage("ReduceSpeed", percent);
	}
}

/* Item list with Indexes:
 * Exhaust Pipe = 0
 * Transmission Box = 1
 * Tire = 2
 * 
 *
 *
 *
 *
 *
 *
*/