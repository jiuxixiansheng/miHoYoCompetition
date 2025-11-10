using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public int orderInRythm;

    public PlayerGameplay playerGameplay;

    UnityEngine.UI.Image image;

    public Sprite spriteQuestion;

    public Sprite spriteCorrect;

    void Start()
    {
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    void Update()
    {
        if(playerGameplay.chosenLandscape != null)
        {
            LandscapeData land = playerGameplay.chosenLandscape.GetComponent<LandscapeData>();
            image.sprite = 
                (land.orderInRythm == orderInRythm) ? spriteCorrect : spriteQuestion;
        }
    }
}
