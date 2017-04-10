using UnityEngine;

namespace Assets.Scripts {

    public class GameManager : MonoBehaviour {

       // public GameObject lifeManager;

        public static GameManager Instance;

        // Private constructor since this is a singleton.
        private GameManager() { }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

		public void Start(){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

		}

        public void EndGame() 
        {
            LifeManager lifeManager = GameObject.FindObjectOfType<LifeManager>() as LifeManager;
            if( lifeManager) lifeManager.TakeLife();
        }
    }
}