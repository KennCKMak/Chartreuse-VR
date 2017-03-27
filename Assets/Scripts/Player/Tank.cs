using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Assets.Scripts {

    public class Tank : MonoBehaviour {

        public Text Minutes;
        public Text Seconds;

        public Image BarImage;

        void Update() {
            float e = Timer.Instance.OxygenTimer / Timer.Instance.DefaultStartingTimer;
            BarImage.fillAmount = e;

            Minutes.text = (int) Timer.Instance.OxygenTimer / 60 + ":";
            Seconds.text = (int) Timer.Instance.OxygenTimer % 60 + "";
        }
    }
}