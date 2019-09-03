    using System;
    using UnityEngine;

        public class Colisions : MonoBehaviour
        {
            public Collider collider;

            private void Start()
            {
                collider = GetComponent<Collider>();
            }

            void OnTriggerEnter(Collider other)
            {
                Debug.Log("collision");
                //Check for a match with the specific tag on any GameObject that collides with your GameObject
                if (other.gameObject.CompareTag("Barrier"))
                {
                    //If the GameObject has the same tag as specified, output this message in the console
                    Debug.Log("Do something else here");
                }
            }
        }
