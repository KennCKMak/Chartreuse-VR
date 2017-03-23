using UnityEngine;
using System.Collections.Generic;

namespace Utils {

    public enum SoundType {
        MainMenu, Other
    }

    public class SoundManager : MonoBehaviour {

        public static SoundManager Instance;
        public AudioSource MainSource;

        public static readonly AudioClip MainMenu;
        public static readonly AudioClip Other;

        private readonly Dictionary<SoundType, AudioClip> _soundLookup = new Dictionary<SoundType, AudioClip>();

        // Private constructor since this is a singleton.
        private SoundManager() { }

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            _soundLookup.Add(SoundType.MainMenu, MainMenu);
            _soundLookup.Add(SoundType.Other, Other);
        }

        public void PlaySound(SoundType type, bool stopCurrentSound) {
            AudioClip clip = _soundLookup[type];
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
