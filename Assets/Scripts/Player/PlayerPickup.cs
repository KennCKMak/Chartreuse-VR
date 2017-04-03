using Assets.Scripts.Pickup;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerPickup : MonoBehaviour
    {

        private Ray ShootRay;
        private RaycastHit ShootHit;
        public float Range = 100f;

        public GameObject Hand;
        public GameObject HeldItem;
        public Text PickupItemText;

        private void FixedUpdate()
        {
            ShootRay.origin = transform.position;
            ShootRay.direction = transform.forward;

            if (Physics.Raycast(ShootRay, out ShootHit, Range))
            {

                GameObject hit = ShootHit.collider.gameObject;

                if (hit.CompareTag("Pickup"))
                {
                    IPickupable pickupable = hit.GetComponent<IPickupable>();
                    PickupItemText.text = "Press E to pickup " + pickupable.GetName();
                    PickupItemText.enabled = true;
                    if (Input.GetKeyDown(KeyCode.E)) {
                        if (HeldItem == null)
                        {

                            // TODO: Put this in a method
                            hit.GetComponent<Collider>().enabled = false;
                            hit.transform.parent = Hand.transform;
                            hit.transform.position = new Vector3(0f,0f,0f);
                            hit.transform.localPosition = new Vector3(0f,0f,0f);
                            HeldItem = hit;
                        } else {
                            HeldItem.transform.parent = null;
                            HeldItem.transform.position = Vector3.zero;

                            hit.GetComponent<Collider>().enabled = false;
                            hit.transform.parent = Hand.transform;
                            hit.transform.position = new Vector3(0f,0f,0f);
                            hit.transform.localPosition = new Vector3(0f,0f,0f);
                            HeldItem = hit;
                        }
                    }
                }
            }
            else
            {
                PickupItemText.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (HeldItem != null)
                {
                    HeldItem.transform.parent = null;
                    HeldItem.transform.position = Vector3.zero;
                    HeldItem.GetComponent<Collider>().enabled = true;
                    HeldItem = null;
                }

            }
        }
    }
}