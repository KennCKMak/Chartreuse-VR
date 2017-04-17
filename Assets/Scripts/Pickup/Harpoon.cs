
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Assets.Scripts.Pickup {

    public class Harpoon : MonoBehaviour, IPickupable 
	{
		private bool holding = false;
		public Rigidbody rb;
		public GameObject Hand;
		public GameObject This;
		public bool Gone = false;
		public bool buttonEnabled = true;
		public PlayerPickup Otherscript;
		public Text ReelInText;
		void Start() {
			rb = GetComponent<Rigidbody>();
			GameObject P = GameObject.FindGameObjectWithTag ("Player");
			Otherscript = P.GetComponent<PlayerPickup> ();
			ReelInText = GameObject.Find ("Canvas").gameObject.transform.FindChild ("Reel in").transform.FindChild ("Text").
				gameObject.GetComponent<Text>();
			ReelInText.enabled = false;
		}
		void Update () {
			if (GameObject.Find ("Player").GetComponent<PlayerPickup>().Pickedup) {
				holding = true;
				Debug.Log ("true");
			}
			if (Input.GetKeyDown (KeyCode.Mouse0) && holding == true && buttonEnabled == true) {
				StartCoroutine(Throw(18.0f, 0.0f, Camera.main.transform.forward,2.0f));
			}
			if (Input.GetKeyDown (KeyCode.Mouse0) && Gone == true && buttonEnabled == true) {
				StartCoroutine(Return(18.0f, 0.0f, Camera.main.transform.forward,2.0f));
			}
		}
			
        public void UseItem() {

        }

        public string GetName()
        {
            return "Harpoon";
        }

		public void OnTriggerEnter(Collider col) 
		{
			Debug.Log ("Collision with something");
			if (col.gameObject.tag == "Creature") {
				string name = col.gameObject.name;
			
				if (name == "MegalodonModel") {
					Shark shark = col.transform.parent.GetComponent<Shark> ();
					shark.takeDamage (10);
					shark.aggressiveness += 15f;
					if (shark.isDead ()) {
						shark.enabled = false;
						col.transform.parent.transform.parent = transform;
						Vector3 pos = col.transform.localPosition;
						pos.x = 0.0f; pos.z = 0.0f;
						col.transform.parent.transform.localPosition = pos;

						col.GetComponent<MeshCollider> ().enabled = false;

					}
				} else 
				{ //other fishes
					Fish fish = col.transform.parent.GetComponent<Fish>();
					fish.takeDamage (15);
					if (fish.isDead ()) {
						fish.enabled = false;
						col.transform.parent.transform.parent = transform;

						Vector3 pos = col.transform.localPosition;
						pos.x = 0.0f; pos.z = 0.0f;
						col.transform.parent.transform.localPosition = pos;

						col.GetComponent<MeshCollider> ().enabled = false;
					}
				}

			} 

		
		}
		IEnumerator Throw(float dist, float width, Vector3 direction, float time) 
		{
			buttonEnabled = false;
			Debug.Log ("started");
			Vector3 pos2 = Hand.transform.position;
			float height = Hand.transform.localPosition.y;
			Quaternion q = Quaternion.FromToRotation (Vector3.forward, direction);
			float timer = 0.0f;
			//rb.AddTorque (0.0f, 400.0f, 0.0f);
			while (timer < time * 0.5)
			{
				float t = Mathf.PI * 2.0f * timer / time - Mathf.PI/2.0f;
				float x = width * Mathf.Cos(t);
				float z = dist * Mathf.Sin (t);
				Vector3 v = new Vector3(x,height,z+dist);
				rb.MovePosition(pos2 + (q * v));
				timer += Time.deltaTime;
				yield return null;
				Gone = true;
				holding = false;
				Otherscript.Pickedup = false;
			}
			Gone = true;
			holding = false;
			Otherscript.Pickedup = false;
			yield return new WaitForSeconds (2.1f);

				buttonEnabled = true;
			ReelInText.enabled = true;
		/*	rb.angularVelocity = Vector3.zero; 
			rb.velocity = Vector3.zero; 
			rb.rotation = Quaternion.identity; 
			rb.MovePosition (pos2);
			This.transform.localPosition = new Vector3(0f,0f,0f);
			This.transform.localRotation = Quaternion.identity;
*/
		}
		IEnumerator Return(float dist, float width, Vector3 direction, float time) 
		{
			buttonEnabled = false;
			Debug.Log ("started");
			Vector3 pos2 = Hand.transform.position;
			float height = Hand.transform.localPosition.y;
			Quaternion q = Quaternion.FromToRotation (Vector3.forward, direction);
			float timer = 0.0f;
			//rb.AddTorque (0.0f, 400.0f, 0.0f);
			while (timer < time && timer > 1.5f)
			{
				float t = Mathf.PI * 2.0f * timer / time ;
				float x = width * Mathf.Cos(t);
				float z = dist * Mathf.Sin (t);
				Vector3 v = new Vector3(x,height,z+dist);
				rb.MovePosition(pos2 + (q * v));
				timer += Time.deltaTime;
				yield return null;
				Gone = false;
				holding = true;
				Otherscript.Pickedup = true;
			}
			rb.angularVelocity = Vector3.zero; 
			rb.velocity = Vector3.zero; 
			rb.rotation = Quaternion.identity; 
			rb.MovePosition (pos2);
			This.transform.localPosition = new Vector3(0f,0f,0f);
			This.transform.localRotation = Quaternion.identity;
			Gone = false;
			holding = true;
			Otherscript.Pickedup = true;
			ReelInText.enabled = false;
			yield return new WaitForSeconds (1.1f);

			buttonEnabled = true;
		}
	}
}
