
﻿using UnityEngine;

namespace Assets.Scripts.Pickup {

    public class Harpoon : MonoBehaviour, IPickupable {

        public void UseItem() {

        }

        public string GetName()
        {
            return "Harpoon";
        }

		public void OnTriggerEnter(Collider col) {
			Debug.Log ("Collision with something");
			if (col.gameObject.tag == "Creature") {
				string name = col.gameObject.name;
			
				if (name == "MegalodonModel") {
					Shark shark = col.transform.parent.GetComponent<Shark> ();
					shark.takeDamage (10);
					if (shark.isDead ()) {
						shark.enabled = false;
						col.transform.parent.transform.parent = transform;

						Vector3 pos = col.transform.localPosition;
						pos.x = 0.0f; pos.z = 0.0f;
						col.transform.parent.transform.localPosition = pos;

						col.GetComponent<MeshCollider> ().enabled = false;

					}
				} else { //other fishes
					Fish fish = col.transform.parent.GetComponent<Fish>();
					fish.takeDamage (15);
					if (fish.isDead ()) {
						fish.enabled = false;
						col.transform.parent.transform.parent = transform;

						Vector3 pos = col.transform.localPosition;
						pos.x = 0.0f; pos.z = 0.0f;
						col.transform.parent.transform.localPosition = pos;

						col.GetComponent<MeshCollider> ().enabled = false;
					}
				}

			} 

		}
	}
}