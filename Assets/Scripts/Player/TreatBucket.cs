using System.Collections;
using UnityEngine;

namespace Assets.Scripts {

    public class TreatBucket : MonoBehaviour {

        public GameObject FishBait;
        public float WaitTime;
        private GameObject bait;
        private float lastThrow;

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                if (Time.time > lastThrow) {
                    bait = Instantiate (FishBait, transform.position, transform.rotation);
                    bait.GetComponent<Rigidbody>().velocity = transform.forward * 4;
                    lastThrow = Time.time + WaitTime;
                }
            }
        }
    }
}
