using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int paused;
    public int level;
    public int selecao;
    public int placeableHighs;
    public int placeableLows;
    public Slider slider;
    public int[] startHighs;
    public int[] startLows;
    public int[] cats;
    public AudioClip success;
    public AudioClip fail;

    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        int[] LstartHighs = { 1, 1, 0, 1, 1, 1, 2 };
        int[] LstartLows = { 1, 1, 1, 1, 1, 2, 2 };
        int[] Lcats = { 2, 2, 1, 4, 2, 3, 2 };
        startHighs = LstartHighs;
        startLows = LstartLows;
        cats = Lcats;
        paused = 1;
        selecao = 0;
        placeableHighs = startHighs[level-1];
        placeableLows = startLows[level-1];
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        success = Resources.Load("success", typeof(AudioClip)) as AudioClip;
        fail = Resources.Load("fail", typeof(AudioClip)) as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider == null)
        {
            slider = GameObject.Find("Slider").GetComponent<Slider>();
        }
        if (paused == 1)
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
        }
        else
        {
            Time.timeScale = slider.value;
        }
    }

    public IEnumerator failLevel()
    {
        fail = Resources.Load("fail", typeof(AudioClip)) as AudioClip;
        GameObject.Find("SFXManager").GetComponent<AudioSource>().clip = fail;
        GameObject.Find("SFXManager").GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(1);
        paused = 1;
        selecao = 0;
        placeableHighs = startHighs[level - 1];
        placeableLows = startLows[level - 1];
        SceneManager.LoadScene("Level" + level);
    }

    public IEnumerator passLevel()
    {
        success = Resources.Load("success", typeof(AudioClip)) as AudioClip;
        GameObject.Find("SFXManager").GetComponent<AudioSource>().clip = success;
        GameObject.Find("SFXManager").GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(2);
        level++;
        paused = 1;
        selecao = 0;
        placeableHighs = startHighs[level - 1];
        placeableLows = startLows[level - 1];
        SceneManager.LoadScene("Level" + level);
    }
}
