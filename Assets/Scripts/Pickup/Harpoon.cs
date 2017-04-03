using UnityEngine;

namespace Assets.Scripts.Pickup {

    public class Harpoon : MonoBehaviour, IPickupable {

        public void UseItem() {

        }

        public string GetName()
        {
            return "Harpoon";
        }
    }
}