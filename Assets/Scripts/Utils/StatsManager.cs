using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
	//This is originally made to hold the array of all food-tagged objects

	public GameObject[] foods;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foods = GameObject.FindGameObjectsWithTag ("Food");
	}
}
