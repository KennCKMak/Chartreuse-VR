using System.Collections;
using UnityEngine;

namespace Assets.Scripts {

    public class TreatBucket : MonoBehaviour {

        public GameObject FishBait;
        public GameObject FishFlock;
        public float WaitTime;
        private GameObject bait;
        private float lastThrow;

        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Pressed left click.");

                if (Time.time > lastThrow) {
                    bait = Instantiate (FishBait, transform.position, transform.rotation);
                    bait.GetComponent<Rigidbody>().velocity = bait.transform.forward * 13;
                    StartCoroutine(StartTask());
                    lastThrow = Time.time + WaitTime;
                }
            }
        }

        private IEnumerator StartTask() {
            yield return new WaitForSeconds(WaitTime + 1);
            GameObject newFish = Instantiate (FishFlock, bait.transform.position, Quaternion.identity);
            FishFlock fishFlock = newFish.GetComponent<FishFlock>();
            if (fishFlock)
            {
                fishFlock.target = bait;
            }
            Destroy(bait);
        }
    }
}
