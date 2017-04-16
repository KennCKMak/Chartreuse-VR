using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : CreatureBase {

	public float aggressiveness;

	public float health;
	public GameObject player;

	// Use this for initialization
	void Awake(){
		Initialize();
	}

	void Start () {
		health = 100;
		player = GameObject.FindGameObjectWithTag ("Player").gameObject;
	}	
	
	// Update is called once per frame
	void Update () {
		CreatureUpdate ();
	}

	protected override void Initialize() {
		CurrentState = CreatureState.Wander;
		targetPosition = transform.position;
		curSpeed = 5.0f; curRotSpeed = 2.0f;
		SetTarget (transform.FindChild("Target").gameObject);
		SetTargetParent (null);

		health = 100;
		movementRange = 100;
		GetNewWanderTarget ();
	}
		

	public override void takeDamage(float damage){
		health -= damage;
		aggressiveness -= 50;
		CheckHealth ();
	}

	protected override void CreatureUpdate() {
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
	}

	protected override void UpdateWanderState() {
		aggressiveness += 0.2f;
		elapsedTime += Time.deltaTime;
		if (aggressiveness >= 150f) { //arbitrary case to initiate chase status
			SwitchCurrentStateTo (CreatureState.Chase);
		} else {
			MoveToward (target.transform.position);
			if (Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) <= 1.0f) { //start lunging
				GetNewWanderTarget();
			}
		}
		if (elapsedTime >= 7) {
			GetNewWanderTarget ();
			elapsedTime = 0;
		}
	}

	protected override void UpdateChaseState(){
		MoveToward (player.transform.position);
		if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) <= 4.0f) { //if reached chase target, attack
			SwitchCurrentStateTo(CreatureState.Attack);
		}
		if (aggressiveness < 150)  //no food, wander
			SwitchCurrentStateTo (CreatureState.Wander);

	}

	protected override void UpdateAttackState(){
		if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) <= 4.0f) { //start lunging
			AttackTarget (player);
		}
		if (Mathf.Abs (Vector3.Distance (player.transform.position, transform.position)) <= 0.2f) { //if hit food, reset the object
			// SCRIPT TO HIT PLAYER HEALTH
			Debug.Log ("Hit player!");
			player.GetComponent<PlayerController> ().TakeDamage (10);
			aggressiveness = 0;
		}

		if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) >= 6.0f && attacking) { //stop lunging
			attacking = false;
			SwitchCurrentStateTo (CreatureState.Chase);
			GetNewWanderTarget ();
		}
	}
}
