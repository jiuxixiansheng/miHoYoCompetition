using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplay : MonoBehaviour
{
    public MoveLandScape moveLandScape;

    public GameObject initialLandscape;
    public GameObject currentLandscape;
    public GameObject chosenLandscape;

    void Start()
    {
        currentLandscape = initialLandscape;
    }


}
