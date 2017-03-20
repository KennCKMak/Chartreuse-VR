using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utils {

    public class Timer : MonoBehaviour {

        // Default starting time in seconds
        public const float DefaultStartingTime = 300; // 5 minutes

        // Timer static instance
        public static Timer Instance;

        // Coroutine tick variable to keep track of countdown.
        private Coroutine TickCoroutine;

        // Current time remaining
        private float Time;

        // Private constructor since this is a singleton.
        private Timer() { }

        void Awake() {

            if (Instance == null) {
                Instance = this;
            }
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void StopTimer() {
            StopCoroutine(TickCoroutine);
        }

        public float GetTimeRemaining() {
            return Time;
        }

        public string GetFormattedTime() {
            return string.Format("{0:D2}:{1:D2}", Time / 60, Time % 60);
        }

        private IEnumerator OnTick() {
            while(true) {
                Time--;

                if(Time == 0) {
                    // TODO: End game here.
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
