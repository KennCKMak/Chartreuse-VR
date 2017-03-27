
using Assets.Scripts;
using UnityEngine;

namespace Utils {

    public class Timer : MonoBehaviour {

        // Timer static instance
        public static Timer Instance;

        // Current time remaining
        public float OxygenTimer = 300; // 5 minutes

        public float DefaultStartingTimer;

        // Private constructor since this is a singleton.
        private Timer() { }

        void Awake() {

            if (Instance == null)
                Instance = this;

            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            DefaultStartingTimer = OxygenTimer;
        }

        void Update() {
            OxygenTimer -= Time.deltaTime;
            if (OxygenTimer <= 0) {
                GameManager.Instance.EndGame();
            }
        }

        public float GetTimeRemaining() {
            return OxygenTimer;
        }

        public string GetFormattedTime() {
            return string.Format("{0:D2}:{1:D2}", OxygenTimer / 60, OxygenTimer % 60);
        }
    }
}
