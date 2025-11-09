using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    public PlayerGameplay playerGameplay;
    public void OnMouseDown()
    {
        Debug.Log("Mouse clicked on: " + gameObject.name);
        playerGameplay.chosenLandscape = gameObject;
    }
}
