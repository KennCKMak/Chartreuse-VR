using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : CreatureBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CreatureUpdate ();
	}

	protected override void UpdateWanderState() {
		if (target.transform.name == "Food") { //arbitrary case to initiate chase status
			SwitchCurrentStateTo (CreatureState.Chase);
		} else {
			MoveToward (target.transform.position);
		}
	}

	protected override void UpdateAttackState(){
		if (Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) <= 2.0f) { //start lunging
			AttackTarget (target);
		}
		if (Mathf.Abs (Vector3.Distance (target.transform.position, transform.position)) <= 0.1f) //if hit food, reset the object
			target.transform.name = "EatenFood";

		if (Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) >= 2.1f && attacking) { //stop lunging
			attacking = false;
			SwitchCurrentStateTo (CreatureState.Chase);
		}
	}
}
