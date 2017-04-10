using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Utils {

    public enum SoundType {
        MainMenu, UnderwaterAmbient
    }

    public class SoundManager : MonoBehaviour {

        public static SoundManager Instance;
        public AudioSource MainSource;

        public AudioClip MainMenu;
        public AudioClip UnderwaterAmbient;

		private string sceneName;

        private readonly Dictionary<SoundType, AudioClip> SoundLookup = new Dictionary<SoundType, AudioClip>();

        // Private constructor since this is a singleton.
        private SoundManager() { }

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            SoundLookup.Add(SoundType.MainMenu, MainMenu);
            SoundLookup.Add(SoundType.UnderwaterAmbient, UnderwaterAmbient);


			sceneName = SceneManager.GetActiveScene ().name;
			switchSong ();
        }
		void Update(){
			if (sceneName != SceneManager.GetActiveScene ().name) {
				sceneName = SceneManager.GetActiveScene ().name;
				switchSong ();
			}
		}

        public void PlaySound(SoundType type) {
            AudioClip clip = SoundLookup[type];
            if (MainSource.isPlaying) {
                MainSource.Stop();
            }
				MainSource.clip = clip;
				MainSource.Play ();
        }

		public void switchSong(){
			if (sceneName == "MainMenu") {
				PlaySound (SoundType.MainMenu);
			} else if (sceneName == "Main") {
				PlaySound (SoundType.UnderwaterAmbient);
			} else
				Debug.Log ("No correct scene");
		}

        public void StopPlaying() {
            MainSource.Stop();
        }
    }
}
