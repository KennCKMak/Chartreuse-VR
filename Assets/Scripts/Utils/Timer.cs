using System.Collections;
using Assets.Scripts;
using UnityEngine;

namespace Utils {

    public class Timer : MonoBehaviour {

        // Timer static instance
        public static Timer Instance;

        // Coroutine tick variable to keep track of countdown.
        private Coroutine _tickCoroutine;

        // Current time remaining
        public int Time = 300; // 5 minutes

        // Private constructor since this is a singleton.
        private Timer() { }

        void Awake() {

            if (Instance == null) {
                Instance = this;
            }
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            _tickCoroutine = StartCoroutine(OnTick());
        }

        public void StopTimer() {
            StopCoroutine(_tickCoroutine);
        }

        public int GetTimeRemaining() {
            return Time;
        }

        public string GetFormattedTime() {
            return string.Format("{0:D2}:{1:D2}", Time / 60, Time % 60);
        }

        private IEnumerator OnTick() {
            while(true) {
                Time--;

                if(Time == 0) {
                    StopTimer();
                    GameManager.Instance.EndGame();
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
