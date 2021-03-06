﻿using Assets.Scripts.Pickup;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerPickup : MonoBehaviour
    {
		public bool Pickedup = false;
        private Ray ShootRay;
        private RaycastHit ShootHit;
        public float Range = 100f;
		public bool Return = false;
        public GameObject Hand;
		public GameObject rope;
        public GameObject HeldItem;
        public Text PickupItemText;
		private void Start(){
			PickupItemText = GameObject.Find ("Canvas").gameObject.transform.
				FindChild ("Pickup").transform.FindChild ("Text").
				gameObject.GetComponent<Text>();
		}


        private void FixedUpdate()
        {
            ShootRay.origin = transform.position;
            ShootRay.direction = transform.forward;

			if (Physics.Raycast(ShootRay, out ShootHit, Range) )
            {

                GameObject hit = ShootHit.collider.gameObject;

				if (hit.CompareTag("Pickup") )
                {
                    IPickupable pickupable = hit.GetComponent<IPickupable>();
                    PickupItemText.text = "Press E to pickup " + pickupable.GetName();
                    PickupItemText.enabled = true;
					if (Input.GetKeyDown(KeyCode.E)) {
						if (HeldItem == null )
                        {
							rope.SetActive(true);
							Pickedup = true;
                            // TODO: Put this in a method
							hit.GetComponent<MeshCollider>().enabled = false;
                            hit.GetComponent<CapsuleCollider>().enabled = true;
                            hit.transform.parent = Hand.transform;
                            //hit.transform.position = new Vector3(0f,0f,0f);
                            hit.transform.localPosition = new Vector3(0f,0f,0f);
							hit.transform.localRotation = Quaternion.identity;
                            HeldItem = hit;

                        } else {
                            HeldItem.transform.parent = null;
                            HeldItem.transform.position = Vector3.zero;

							hit.GetComponent<MeshCollider>().enabled = true;
							hit.GetComponent<CapsuleCollider>().enabled = false;
                            hit.transform.parent = Hand.transform;
                            hit.transform.position = new Vector3(0f,0f,0f);
                            hit.transform.localPosition = new Vector3(0f,0f,0f);
                            HeldItem = hit;

                        }
                    }
                }
            }
            else
            {
                PickupItemText.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (HeldItem != null)
                {
                    HeldItem.transform.parent = null;
                    //HeldItem.transform.position = Vector3.zero;
                    HeldItem.GetComponent<Collider>().enabled = true;
                    HeldItem = null;
					Pickedup = false;
                }
				Return = false;

            }
        }

	}
}