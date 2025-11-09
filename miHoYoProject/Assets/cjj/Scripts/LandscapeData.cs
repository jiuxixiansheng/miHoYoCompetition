using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LandscapeData : MonoBehaviour
{
    [SerializeField] public List<GameObject> Plugs;

    public int orderInRythm;
    private GameObject defaultPlug;
    private LandscapeData connectScape;
    public bool correctlyConnected = false;

    void Start()
    {
        defaultPlug = Plugs[0];
    }
}
