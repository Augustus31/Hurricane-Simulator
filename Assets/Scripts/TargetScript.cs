using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public Sprite grayStar;
    // Start is called before the first frame update
    void Start()
    {
        grayStar = Resources.Load("gstar", typeof(Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Call game-ending function
        GetComponent<SpriteRenderer>().sprite = grayStar;
        Debug.Log("Level Complete");
    }
}
