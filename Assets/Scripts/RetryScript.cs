using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{
    public GameController control;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        control.paused = 1;
        control.selecao = 0;
        control.placeableHighs = control.startHighs[control.level - 1];
        control.placeableLows = control.startLows[control.level - 1];
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
