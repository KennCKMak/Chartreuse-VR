using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlock : MonoBehaviour {

	public GameObject targetFlock;//default movement
	public GameObject targetFood; //gameobject for food
	protected Vector3 StartLocation;
	protected GameObject[] Fishes;
	protected Vector3[] fishLocations;
	public GameObject FishPrefab;
	protected int FishCount;
	GameObject[] foods;
	protected float elapsedTime;

	// Use this for initialization
	void Start () {
		StartLocation = transform.position;
		targetFlock = transform.FindChild ("FlockTarget").gameObject;
		targetFlock.transform.parent = null;
		FishCount = transform.childCount;
		Fishes = new GameObject[FishCount];

		GetFishLocations ();
		for (int i = 0; i < FishCount; i++) {
			GameObject newFish = Instantiate (FishPrefab, fishLocations [i], Quaternion.identity) as GameObject;
			newFish.GetComponent<Fish> ().setIsFlocking (true);
			newFish.GetComponent<Fish> ().MoveTarget (fishLocations [i]);
			newFish.GetComponent<Fish> ().SetTargetParent (this.transform);

			Fishes [i] = newFish;

		}
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
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
}
