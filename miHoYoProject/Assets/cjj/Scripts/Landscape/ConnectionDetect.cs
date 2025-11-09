using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionDetect : MonoBehaviour
{

    public PlayerGameplay playerGameplay;
    void OnTriggerEnter(Collider other)
    {
        if (gameObject != playerGameplay.currentLandscape) return;
        if (other.gameObject != playerGameplay.chosenLandscape) return;
        LandscapeData landscapeDataCur = gameObject.GetComponent<LandscapeData>();
        LandscapeData landscapeDataNxt = other.gameObject.GetComponent<LandscapeData>();
        landscapeDataCur.correctlyConnected = (
            (landscapeDataCur.orderInRythm + 1) % 5 == landscapeDataNxt.orderInRythm);
        Debug.Log("Collision with:" + other.gameObject.name);
    }
}
