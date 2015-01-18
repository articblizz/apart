using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


	bool onGround;
	public float Speed = 5;
	public float RunSpeed = 3;
	public float JumpHeight = 150;
	public float MaxVelocityChange = 4;

	public GameObject CamHolder;

	private bool left;
	private bool right;

	// Use this for initialization
	void Start () {
		left = true;
		right = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void HeadBob()
	{
			CamHolder.audio.pitch = Random.Range(0.3f,1.7f);
			if(Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") == 1 && Input.GetAxis("Horizontal") == 0 && canSprint)
			{
				CamHolder.animation["walkRight"].speed = 1f;
				CamHolder.animation["walkLeft"].speed = 1f;
			}
			else
			{
				CamHolder.animation["walkRight"].speed = 0.5f;
				CamHolder.animation["walkLeft"].speed = 0.5f;
			}
			if(left == true){
				if(!CamHolder.animation.isPlaying){//Waits until no animation is playing to play the next
					CamHolder.audio.Play();
					CamHolder.animation.Play("walkLeft");
					left = false;
					right = true;
				}
			}
			if(right == true){
				if(!CamHolder.animation.isPlaying){
					CamHolder.audio.Play();
					CamHolder.animation.Play("walkRight");
					right = false;
					left = true;
				}
			}
	}

	bool canSprint = true;
	float defSpeed;

	float currentReduction;
	float currentRedPercent;

	void ReduceSpeed(float percent)
	{
		defSpeed = Speed;
		currentRedPercent = percent;
		currentReduction = (Speed * (percent / 100));
		Speed -= currentReduction;
		canSprint = false;
	}

	void ResetSpeed()
	{
		canSprint = true;
		currentReduction = 0;
		currentRedPercent = 0;
		Speed = defSpeed;
	}

	void OnGUI()
	{
		if(Input.GetKey(KeyCode.LeftShift) && canSprint)
			GUI.Label(new Rect(10,25,300,25), "Movementspeed: " + (Speed+RunSpeed) + " (-" + currentReduction + ", " + currentRedPercent + "%)");
		else
			GUI.Label(new Rect(10,25,300,25), "Movementspeed: " + Speed + " (-" + currentReduction + ", " + currentRedPercent + "%)");
	}

	void FixedUpdate()
	{
		if(onGround)
		{
			rigidbody.angularVelocity = new Vector3(0,0,0);

			var finalSpeed = Speed;

			if(Input.GetKey(KeyCode.LeftShift) && canSprint)
			{
				if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetAxis("Horizontal") == 0)
					finalSpeed += RunSpeed;
			}
			
			var targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = Camera.main.transform.TransformDirection(targetVelocity);
			targetVelocity *= finalSpeed;

			if(targetVelocity.x != 0 || targetVelocity.z != 0)
			{
				HeadBob();
			}
			else
				CamHolder.animation.Stop();

			var v = rigidbody.velocity;
			var velocityChange = (targetVelocity-v);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
			velocityChange.y = 0;

			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
		if(Input.GetButtonDown("Jump") && onGround)
		{
			rigidbody.AddForce(transform.up * JumpHeight);
		}

		onGround = false;
	}

	void OnCollisionStay(Collision col)
	{
		if(col.transform.tag != "Not Ground")
		{
			onGround = true;
		}
	}
}
