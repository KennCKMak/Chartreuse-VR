using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Utils {

    public enum SoundType {
        MainMenu, Other
    }

    public class SoundManager : MonoBehaviour {

        public static SoundManager Instance;
        public AudioSource MainSource;

        public static readonly AudioClip MainMenu;
        public static readonly AudioClip Other;

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
            SoundLookup.Add(SoundType.Other, Other);
        }

        public void PlaySound(SoundType type, bool stopCurrentSound) {
            AudioClip clip = SoundLookup[type];
            if (stopCurrentSound && MainSource.isPlaying) {
                MainSource.Stop();
            }
            MainSource.clip = clip;
            MainSource.Play();
        }

        public void StopPlaying() {
            MainSource.Stop();
        }
    }
}
