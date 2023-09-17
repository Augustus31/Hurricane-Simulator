using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHoverScript : MonoBehaviour
{
    public SpriteRenderer rend;
    public GameController control;
    public Texture2D yes;
    public Texture2D no;
    public GameObject high;
    public GameObject low;
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
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        GameObject furacao = GameObject.Find("Furacao");
        if (Mathf.Abs(((Vector2)furacao.transform.position - worldPosition).magnitude) > 175)
        {
            if (control.selecao == 1)
            {
                GameObject.Instantiate(high, worldPosition, Quaternion.identity);
                control.selecao = 0;
                control.placeableHighs--;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            else if (control.selecao == 2)
            {
                GameObject.Instantiate(low, worldPosition, Quaternion.identity);
                control.selecao = 0;
                control.placeableLows--;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }

    private void OnMouseOver()
    {
        if(control.selecao != 0)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            GameObject furacao = GameObject.Find("Furacao");
            if (Mathf.Abs(((Vector2)furacao.transform.position - worldPosition).magnitude) > 175)
            {
                Vector2 cursorOffset = new Vector2(yes.width / 2, yes.height / 2);
                Cursor.SetCursor(yes, cursorOffset, CursorMode.Auto);

            }
            else
            {
                Vector2 cursorOffset = new Vector2(no.width / 2, no.height / 2);
                Cursor.SetCursor(no, cursorOffset, CursorMode.Auto);
            }
        }
        
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
