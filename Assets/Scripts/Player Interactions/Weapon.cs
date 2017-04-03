using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	private int Dmg; //damage or "rage" it inflicts on shark
	private int BluntDmg; //possibly damage for using the wood end to enrage shark
	public bool pickedUp = false;
	public Transform player;
	private GameObject weapon;

	// Use this for initialization
	void Start () {

		weapon = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if (pickedUp == true) 
		{
			this.transform.parent = player;
		} 
		else
		{
			this.transform.parent = null; //this becomes cage so that it moves with the cage
		}
		if (Input.GetMouseButtonDown(0))
		{
			//animate stab
		}

	}
}
