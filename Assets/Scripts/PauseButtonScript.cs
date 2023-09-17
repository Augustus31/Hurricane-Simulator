using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonScript : MonoBehaviour
{
    public GameController control;
    public Sprite pause;
    public Sprite play;
    public SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameController").GetComponent<GameController>();
        pause = Resources.Load("pause", typeof(Sprite)) as Sprite;
        play = Resources.Load("play", typeof(Sprite)) as Sprite;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (control.paused == 1)
            {
                control.paused = 0;
                rend.sprite = pause;

            }
            else
            {
                control.paused = 1;
                rend.sprite = play;
            }
        }
    }

    private void OnMouseDown()
    {
        if(control.paused == 1)
        {
            control.paused = 0;
            rend.sprite = pause;

        }
        else
        {
            control.paused = 1;
            rend.sprite = play;
        }
    }
}
