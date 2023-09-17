using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighButtonScript : MonoBehaviour
{
    public Sprite unlit;
    public Sprite lit;
    public SpriteRenderer rend;
    public GameController control;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        control = GameObject.Find("GameController").GetComponent<GameController>();
        unlit = Resources.Load("highnotext", typeof(Sprite)) as Sprite;
        lit = Resources.Load("hlHigh", typeof(Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(control.selecao == 1)
        {
            rend.sprite = lit;
        }
        else
        {
            rend.sprite = unlit;
        }
    }

    private void OnMouseDown()
    {
        if(control.selecao == 0 && control.placeableHighs > 0)
        {
            rend.sprite = lit;
            control.selecao = 1;
        }
    }

}
