using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float floatForce = 2;
	public float moveSpeed = 10;
	public float rotSpeed = 10;
	public float waveSpeed = 1;
	float wavePos = 0;
	public Rigidbody rigB;

	// Use this for initialization
	void Start () {
		rigB = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
	    CameraMovement();
		VerticalInput();
		SwimInput();
		Waves();
		//Debug.Log (rigB.velocity.x);
	}

    private void CameraMovement() {

    }

	void VerticalInput() {
		if (Input.GetKey (KeyCode.Space)) {
			if (rigB.velocity.y <= 2)
				rigB.AddForce (new Vector3 (0, floatForce, 0));
		} else if (Input.GetKey (KeyCode.LeftShift)) {
			if (rigB.velocity.y >= -2)
				rigB.AddForce (new Vector3 (0, -floatForce, 0));
		}
		else {
			if (rigB.velocity.y <= 0.1 && rigB.velocity.y >= -0.1)
				rigB.velocity = new Vector3 (rigB.velocity.x, 0, rigB.velocity.z);
			else if (rigB.velocity.y >= 0)
				rigB.AddForce (new Vector3 (0, -floatForce, 0));
			else if (rigB.velocity.y <= 0)
				rigB.AddForce (new Vector3 (0, floatForce, 0));
		}
	}

	void SwimInput() {
		if (Input.GetKey (KeyCode.W))
			rigB.AddForce (transform.forward * moveSpeed);
		else if (Input.GetKey (KeyCode.S))
			rigB.AddForce (-transform.forward * moveSpeed);
	/*else {
			if (rigB.velocity.x <= 0.1 && rigB.velocity.x >= -0.1) {
				Debug.Log ("1");
				rigB.velocity = new Vector3 (0, rigB.velocity.y, rigB.velocity.z);
			} else
				if (rigB.velocity.x > 0) {
				Debug.Log ("2");
				rigB.AddForce (new Vector3 (-moveSpeed, 0, 0));
			} else if (rigB.velocity.x < 0) {
				Debug.Log ("3");
				rigB.AddForce (new Vector3 (moveSpeed, 0, 0));
			}
		}*/
		
		if (Input.GetKey (KeyCode.D))
			rigB.AddForce (transform.right * moveSpeed);
			//transform.Rotate (new Vector3(0, rotSpeed, 0) * Time.deltaTime);
		if (Input.GetKey (KeyCode.A))
			rigB.AddForce (transform.right * -moveSpeed);
			//transform.Rotate (new Vector3(0,-rotSpeed, 0) * Time.deltaTime);
	}

	void Waves() {
		wavePos += waveSpeed * Time.deltaTime;
		transform.Translate(new Vector3(0, Mathf.Sin(wavePos) / 600, 0));
	}
}
