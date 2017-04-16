using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonCollide : MonoBehaviour {

	GameObject harpoon;
	GameObject creature;
	private Component script;

	void Start(){
		script = GetComponent<HarpoonCollide> ();
	}
	
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Creature") {

			if (col.gameObject.name == "MegalodanModel") {
				Debug.Log ("hit shark");

			}

		}
	
	}
}
