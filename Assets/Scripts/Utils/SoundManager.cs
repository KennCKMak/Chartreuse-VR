using UnityEngine;

namespace Utils {

    public enum SoundType {
        MainMenu, Other, Idk
    }

    public class SoundManager : MonoBehaviour {

        public static SoundManager Instance;

        // Private constructor since this is a singleton.
        private SoundManager() { }

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void PlaySound(SoundType type) {

        }
    }
}