using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidebarScript : MonoBehaviour
{
    public SpriteRenderer rend;
    public GameController control;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        control = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(control.selecao != 0)
        {
            control.selecao = 0;
        }
    }
}
