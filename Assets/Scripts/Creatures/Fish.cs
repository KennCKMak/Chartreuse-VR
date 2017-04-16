using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : CreatureBase {

	public GameObject[] foods;


	// Use this for initialization
	void Awake () {
		Initialize ();

	}
	
	// Update is called once per frame
	void Update () {
		CreatureUpdate ();
	}

	protected virtual void CreatureUpdate() {
		switch (CurrentState) {
		case CreatureState.Wander:
			UpdateWanderState ();
			break;
		case CreatureState.Chase:
			UpdateChaseState();
			break;
		case CreatureState.Attack:
			UpdateAttackState ();
			break;
		case CreatureState.Dead:
			break;
		}
		elapsedTime += Time.deltaTime;
	}

	protected override void UpdateWanderState() {
		FindFood ();
		if (targetFood.transform.tag == "Food") { //arbitrary case to initiate chase status
			SwitchCurrentStateTo (CreatureState.Chase);
		} else {
			SetTargetFood (target);
			MoveToward (target.transform.position);
		}
	}

	protected virtual void UpdateChaseState(){
		if (targetFood.transform.tag == "Food"){  //no food, wander
			MoveToward (targetFood.transform.position);
			if (Mathf.Abs (Vector3.Distance (targetFood.transform.position, transform.position)) <= 2.0f) { //if reached chase target, attack
				SwitchCurrentStateTo (CreatureState.Attack);
			}
		}
		else {
			SwitchCurrentStateTo (CreatureState.Wander);
			SetTargetFood (target);
			GetNewWanderTarget ();
		}

	}

	protected override void UpdateAttackState(){
		if (targetFood.transform.tag == "Food") {
			if (Mathf.Abs (Vector3.Distance (targetFood.transform.position, transform.position)) <= 2.0f) { //start lunging
				AttackTarget (targetFood);
			}
			if (Mathf.Abs (Vector3.Distance (targetFood.transform.position, transform.position)) <= 0.1f) {//if hit food, reset the object
				targetFood.transform.name = "EatenFood";
				targetFood.tag = "Untagged";
				Destroy (targetFood.gameObject, 2.0f);
				targetFood = target;
			}

			if (Mathf.Abs (Vector3.Distance (targetFood.transform.position, transform.position)) >= 2.1f && attacking) { //stop lunging
				attacking = false;
				SwitchCurrentStateTo (CreatureState.Chase);
			}
		} else {
			SetTargetFood (target);
			SwitchCurrentStateTo (CreatureState.Wander);
			GetNewWanderTarget ();
		}
			
	}

	void FindFood(){
		foods = GameObject.Find ("Game Manager").gameObject.GetComponent<StatsManager> ().foods;
		if (foods != null) {
			float searchDistance = 75;
			for (int i = 0; i < foods.Length; i++) {
				float dist = Vector3.Distance (foods [i].transform.position, transform.position);
				if (dist < searchDistance) {
					targetFood = foods [i];
					searchDistance = dist;
				}
			}
		} else {
			targetFood = null;
		}
	}
	public void setIsFlocking(bool value){
		flocking = value;
	}
}
