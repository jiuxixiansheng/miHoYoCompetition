using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered bound: " + other.gameObject.name);

        if (other.gameObject.tag == "boundary")
        {
            Debug.Log("Player has entered the boundary area.");
            Destroy(gameObject);
        }
    }
}
