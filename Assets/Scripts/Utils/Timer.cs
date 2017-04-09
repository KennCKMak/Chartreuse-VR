
using Assets.Scripts;
using UnityEngine;

    public class Timer : MonoBehaviour {

        // Timer static instance
        public static Timer Instance;

        // Current time remaining
        public float OxygenTimer;
        public float DefaultStartingTimer = 300;

        // Private constructor since this is a singleton.
        private Timer() { }

        void Start()
        {
            OxygenTimer = DefaultStartingTimer;

        }
        void Awake() {

            if (Instance == null)
                Instance = this;

            else if (Instance != this)
                Destroy(gameObject);

          //  DontDestroyOnLoad(gameObject);
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

    public void resetTimer()
    {
        OxygenTimer = DefaultStartingTimer;
    }
    }

