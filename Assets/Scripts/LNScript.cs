using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LNScript : MonoBehaviour
{
    public GameController control;
    public TextMeshProUGUI textObject;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameController").GetComponent<GameController>();
        textObject = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textObject.text = "x " + control.placeableLows;
    }
}
