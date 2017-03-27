using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlock : MonoBehaviour {

	protected GameObject target;
	protected Vector3 StartLocation;
	protected GameObject[] Fishes;
	protected Vector3[] fishLocations;
	public GameObject FishPrefab;
	protected int FishCount;

	protected float elapsedTime;

	// Use this for initialization
	void Start () {
		StartLocation = transform.position;
		target = transform.FindChild ("FlockTarget").gameObject;
		target.transform.parent = null;
		FishCount = transform.childCount;
		Fishes = new GameObject[FishCount];

		GetFishLocations ();
		for (int i = 0; i < FishCount; i++) {
			GameObject newFish = Instantiate (FishPrefab, fishLocations [i], Quaternion.identity) as GameObject;
			newFish.GetComponent<Fish> ().SetTarget (newFish.transform.GetChild (0).gameObject);
			newFish.GetComponent<Fish> ().MoveTarget (fishLocations [i]);
			newFish.GetComponent<Fish> ().SetTargetParent (this.transform);
			Fishes [i] = newFish;
		}

	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();

		elapsedTime += Time.deltaTime;
		if (elapsedTime > 8.0f) {
			target.name = "Food";
			target.transform.position = 
				(new Vector3 (StartLocation.x + (Random.Range (-10.0f, 10.0f)), StartLocation.y, StartLocation.z + (Random.Range (-10.0f, 10.0f))));
			elapsedTime = 0;
		}
		GoToFood (target);
	}

	void Move(){
		transform.RotateAround (StartLocation, Vector3.up, 10 * Time.deltaTime);
	}

	void GetFishLocations(){
		fishLocations = new Vector3[FishCount];
		for (int i = 0; i < FishCount; i++) {
			fishLocations [i] = transform.GetChild (i).transform.position;
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	void GoToFood(GameObject food){
		if (food.transform.name == "Food") {
			food.SetActive (true);
			for (int i = 0; i < FishCount; i++) {
				Fishes[i].GetComponent<Fish> ().SetTarget (food);
			}
		} else if (food.transform.name == "EatenFood") {
			food.SetActive (false);
			for (int i = 0; i < FishCount; i++) {
				Fishes[i].GetComponent<Fish> ().SetTarget (transform.GetChild(i).gameObject);
			}
		}
	}



}
