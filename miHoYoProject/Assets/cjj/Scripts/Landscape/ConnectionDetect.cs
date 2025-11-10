using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionDetect : MonoBehaviour
{

    public PlayerGameplay playerGameplay;

    GameObject nowLand;

    void Start()
    {
        // nowLand = playerGameplay.currentLandscape;
    }
    void OnTriggerEnter(Collider other)
    {
        // if (gameObject != playerGameplay.currentLandscape) return;
        if (other.gameObject != playerGameplay.chosenLandscape) return;
        LandscapeData landscapeDataCur = gameObject.GetComponent<LandscapeData>();
        LandscapeData landscapeDataNxt = other.gameObject.GetComponent<LandscapeData>();
        landscapeDataCur.correctlyConnected += (
            ((landscapeDataCur.orderInRythm + 1) % 5 == landscapeDataNxt.orderInRythm)) || 
            (landscapeDataNxt.tag == "Final") ? 1 : 0;
        Debug.Log("Collision with:" + other.gameObject.name);
        if (landscapeDataCur.correctlyConnected > 0)
        {
            Debug.Log("Landscapes are correctly connected.");
            // playerGameplay.currentLandscape = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // if (gameObject != playerGameplay.currentLandscape) return;
        if (other.gameObject != playerGameplay.chosenLandscape) return;
        LandscapeData landscapeDataCur = gameObject.GetComponent<LandscapeData>();
        LandscapeData landscapeDataNxt = other.gameObject.GetComponent<LandscapeData>();
        landscapeDataCur.correctlyConnected -= (
            (landscapeDataCur.orderInRythm + 1) % 5 == landscapeDataNxt.orderInRythm) ? 1 : 0;
        if (landscapeDataCur.correctlyConnected <= 0)
        {
            Debug.Log("Landscapes are no longer correctly connected.");
            // playerGameplay.currentLandscape = nowLand;
        }
    }
}
