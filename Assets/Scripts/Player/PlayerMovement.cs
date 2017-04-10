using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float floatForce = 2;
	public float moveSpeed = 6;
	public float rotSpeed = 10;
	public float slowingSpeed = 0.2f;
	public float waveSpeed = 1;
	float wavePos = 0;
	public Rigidbody rigB;

	// Use this for initialization
	void Start () {
		rigB = this.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		SwimSpeedSlowing ();
		VerticalInput ();
		SwimInput ();


		Waves ();
	}

	void SwimSpeedSlowing(){
		rigB.velocity = rigB.velocity * 0.95f;
	}

	void VerticalInput() {
		if (Input.GetKey (KeyCode.Space)) {
			if (rigB.velocity.y <= 2)
				rigB.velocity += (new Vector3 (0, floatForce, 0));
		} else if (Input.GetKey (KeyCode.LeftShift)) {
			if (rigB.velocity.y >= -2)
				rigB.velocity += (new Vector3 (0, -floatForce, 0));
		}
		/*else {
			if (rigB.velocity.y <= 0.1 && rigB.velocity.y >= -0.1)
				rigB.velocity = new Vector3 (rigB.velocity.x, 0, rigB.velocity.z);
			else if (rigB.velocity.y >= 0)
				rigB.velocity += (new Vector3 (0, -floatForce, 0));
			else if (rigB.velocity.y <= 0)
				rigB.velocity += (new Vector3 (0, floatForce, 0));
		}*/
	}

	void SwimInput() {
		if (Input.GetKey (KeyCode.W))
			rigB.velocity += (transform.forward * moveSpeed);
		else if (Input.GetKey (KeyCode.S))
			rigB.velocity += (-transform.forward * moveSpeed);


		if (Input.GetKey (KeyCode.D))
			rigB.velocity += (transform.right * moveSpeed);
		//transform.Rotate (new Vector3(0, rotSpeed, 0) * Time.deltaTime);
		else if (Input.GetKey (KeyCode.A))
			rigB.velocity += (-transform.right * moveSpeed);
		//transform.Rotate (new Vector3(0,-rotSpeed, 0) * Time.deltaTime);
		SpeedControl ();


	}

	void SpeedControl(){
		if (rigB.velocity.magnitude > moveSpeed) {
			//Debug.Log (rigB.velocity);
			rigB.velocity = rigB.velocity.normalized * moveSpeed;
		}
	}


	void Waves() {
		wavePos += waveSpeed * Time.deltaTime;
		transform.Translate(new Vector3(0, Mathf.Sin(wavePos) / 400, 0));
	}
}
