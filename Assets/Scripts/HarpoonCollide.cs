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
			transform.parent = col.transform;
			Debug.Log ("collision with harpoon");

		}
	
}
}
