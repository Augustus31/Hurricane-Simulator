using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HurricaneScript : MonoBehaviour
{
    public float strength;
    public int category;
    public float x;
    public float y;
    public SpriteRenderer rend;
    public GameController gc;
    public Rigidbody2D rb;
    public Texture2D map;
    public Sprite td;
    public Sprite ts;
    public Sprite c1;
    public Sprite c2;
    public Sprite c3;
    public Sprite c4;
    public Sprite c5;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        float[] startingStrengths = {65, 40, 55, 50, 75, 45, 55};
        strength = startingStrengths[gc.level - 1];
        category = 0;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity.Set(-0.3f, 0);
        map = GameObject.Find("Map").GetComponent<SpriteRenderer>().sprite.texture;
        rend = GetComponent<SpriteRenderer>();
        td = Resources.Load("td", typeof(Sprite)) as Sprite;
        ts = Resources.Load("ts0", typeof(Sprite)) as Sprite;
        c1 = Resources.Load("cat1", typeof(Sprite)) as Sprite;
        c2 = Resources.Load("cat2", typeof(Sprite)) as Sprite;
        c3 = Resources.Load("cat3", typeof(Sprite)) as Sprite;
        c4 = Resources.Load("cat4", typeof(Sprite)) as Sprite;
        c5 = Resources.Load("cat5", typeof(Sprite)) as Sprite;
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        //DIRECTION CALCULATION
        float n = Mathf.Pow(0.2f, Time.deltaTime);
        Vector2 residual = rb.velocity * n;
        Vector2 original = new Vector2(0,0);
        x = transform.position.x;
        y = transform.position.y;

        //Trade Winds
        float differential = y + 215;
        if (((-1 / 1200f) * Mathf.Pow(differential, 2)+55) > 0)
        {
            original.x -= ((-1 / 1200f) * Mathf.Pow(differential, 2)+55);
        }

        //Westerlies
        float differential2 = y - 293;
        if (((-1 / 1200f) * Mathf.Pow(differential2, 2) + 55) > 0)
        {
            original.x += ((-1 / 1200f) * Mathf.Pow(differential2, 2) + 55);
            if (x > 500)
            {
                original.x += 30;
            }
        }
        

        //Monsoon
        float diffx = x + 720;
        float diffy = y + 10;
        if(Mathf.Pow(diffx, 2) + Mathf.Pow(diffy, 2) < Mathf.Pow(260f, 2))
        {
            Vector2 monsoon = new Vector2(20, 68);
            float distance = 1 - (Mathf.Pow(diffx, 2) + Mathf.Pow(diffy, 2)) / Mathf.Pow(260f, 2);
            Vector2 power = monsoon * distance;
            /*if(power.magnitude > 50)
            {
                power.Set(9.8f, 49f);
            }*/
            original += power;

        }

        //High Pressure
        GameObject[] highs = GameObject.FindGameObjectsWithTag("High");
        foreach(GameObject high in highs)
        {
            float diffhx = x - high.transform.position.x;
            float diffhy = y - high.transform.position.y;
            Vector2 temporario = new Vector2(diffhy, -1 * diffhx);
            temporario.Normalize();
            if(Mathf.Sqrt(Mathf.Pow(diffhx,2) + Mathf.Pow(diffhy,2)) < 200)
            {
                temporario = temporario * 60;
            }
            else
            {
                float multiplier = ((560 - Mathf.Sqrt(Mathf.Pow(diffhx, 2) + Mathf.Pow(diffhy, 2))) * 1 / 6);
                if(multiplier > 0)
                {
                    temporario = temporario * multiplier;
                }
            }

            if(Mathf.Sqrt(Mathf.Pow(diffhx, 2) + Mathf.Pow(diffhy, 2)) < 100)
            {
                Vector2 oppose = new Vector2(diffhx, diffhy);
                temporario += oppose.normalized * 1000 / oppose.magnitude;
            }

            original += temporario;
        }

        //Low Pressure
        GameObject[] lows = GameObject.FindGameObjectsWithTag("Low");
        foreach (GameObject low in lows)
        {
            float difflx = x - low.transform.position.x;
            float diffly = y - low.transform.position.y;
            Vector2 temporario = new Vector2(-1 * diffly, difflx);
            temporario.Normalize();
            if (Mathf.Sqrt(Mathf.Pow(difflx, 2) + Mathf.Pow(diffly, 2)) < 150)
            {
                temporario = temporario * 50;
            }
            else
            {
                float multiplier = ((560 - Mathf.Sqrt(Mathf.Pow(difflx, 2) + Mathf.Pow(diffly, 2))) * 1 / 6);
                if (multiplier > 0)
                {
                    temporario = temporario * multiplier;
                }
            }

            if (Mathf.Sqrt(Mathf.Pow(difflx, 2) + Mathf.Pow(diffly, 2)) < 100)
            {
                Vector2 oppose = new Vector2(difflx, diffly);
                temporario += (oppose.normalized * 1000 / oppose.magnitude);
            }

            Debug.Log(temporario);
            original += temporario;
        }

        if(original.magnitude > 80 && original.magnitude < 120)
        {
            Vector2 reverse = (-1 * original).normalized;
            original += (reverse * (1 / 80f) * (float)Math.Pow(original.magnitude - 80, 2));
        }
        else if(original.magnitude > 120)
        {
            Vector2 reverse = (-1 * original).normalized;
            original += (reverse * (original.magnitude-120));
        }

        original = original * (1 - n);
        rb.velocity = original + residual;

        //INTENSITY

        //Favorability
        if(x < 671 && x > -959 && y > -539 && y < 539)
        {
            Color32 col = map.GetPixel((int)Mathf.Round(x + 960), (int)Mathf.Round(y + 540));
            if(col.g > 50 && col.r < 50 && col.b < 50) //Green
            {
                strength += Time.deltaTime * (-1 / 3500f * Mathf.Pow(strength, 2) + 12);
                //Debug.Log("G");
            }
            else if(col.g > 200 && col.r > 200) //Yellow
            {
                strength += Time.deltaTime * (-1/ 1800f * Mathf.Pow(strength, 2) + 10);
                //Debug.Log("Y");
            }
            else if(col.g > 150 && col.r > 200) //Orange
            {
                strength += Time.deltaTime * (-1/ 800f * Mathf.Pow(strength, 2) + 0.2f);
                //Debug.Log("O");
            }
            else if(col.r > 200) //Red
            {
                strength += Time.deltaTime * (-1 / 420f * Mathf.Pow(strength, 2) + 1f);
                //Debug.Log("R");
            }
            else if(col.r < 20 && col.g < 20 && col.b < 20) //Black (land)
            {
                strength += Time.deltaTime * (-1 / 400f * Mathf.Pow(strength, 2) + 1f);
                //Debug.Log(-1 / 400 * Mathf.Pow(strength, 2) + 1f);
                
            }

            //Nearby land
            Color32[] pts = new Color32[4];
            try
            {
                pts[0] = map.GetPixel((int)Mathf.Round(x + 960 + 40), (int)Mathf.Round(y + 540));
            }
            catch(Exception e)
            {
                pts[0] = new Color32(255, 255, 0, 255);
            }

            try
            {
                pts[1] = map.GetPixel((int)Mathf.Round(x + 960 - 40), (int)Mathf.Round(y + 540));
            }
            catch (Exception e)
            {
                pts[1] = new Color32(255, 255, 0, 255);
            }

            try
            {
                pts[2] = map.GetPixel((int)Mathf.Round(x + 960), (int)Mathf.Round(y + 540 + 40));
            }
            catch (Exception e)
            {
                pts[2] = new Color32(255, 255, 0, 255);
            }

            try
            {
                pts[3] = map.GetPixel((int)Mathf.Round(x + 960 + 40), (int)Mathf.Round(y + 540 - 40));
            }
            catch (Exception e)
            {
                pts[3] = new Color32(255, 255, 0, 255);
            }

            int landpts = 0;
            foreach(Color32 colorem in pts)
            {
                if(colorem.r < 20 && colorem.g < 20 && colorem.b < 20)
                {
                    landpts++;
                }
            }
            if(landpts == 0)
            {
                strength += Time.deltaTime;
            }
            else if(landpts < 3)
            {
                strength -= Time.deltaTime;
            }
            else
            {
                strength -= 3 * Time.deltaTime;
            }

            //Debug.Log(x + " " + y);

        }
        if(strength < 37)
        {
            category = -1;
            rend.sprite = td;
            rend.size = new Vector2(60, 60);
            gameObject.GetComponent<Animator>().enabled = false;
            transform.rotation = Quaternion.identity;
            GameObject.Find("Hurricane").GetComponent<TextMeshProUGUI>().text = "Tropical Depression";
            if (!dead)
            {
                dead = true;
                StartCoroutine(gc.failLevel());
            }
            
        }
        else if(strength < 74)
        {
            category = 0;
            rend.sprite = ts;
            rend.size = new Vector2(60, 60);
            gameObject.GetComponent<Animator>().enabled = true;
            GameObject.Find("Hurricane").GetComponent<TextMeshProUGUI>().text = "Tropical Storm";
        }
        else if (strength < 96)
        {
            category = 1;
            rend.sprite = c1;
            rend.size = new Vector2(60, 60);
            GameObject.Find("Hurricane").GetComponent<TextMeshProUGUI>().text = "Cat 1 Hurricane";
        }
        else if(strength < 111)
        {
            category = 2;
            rend.sprite = c2;
            rend.size = new Vector2(60, 60);
            GameObject.Find("Hurricane").GetComponent<TextMeshProUGUI>().text = "Cat 2 Hurricane";
        }
        else if(strength < 130)
        {
            category = 3;
            rend.sprite = c3;
            rend.size = new Vector2(60, 60);
            GameObject.Find("Hurricane").GetComponent<TextMeshProUGUI>().text = "Cat 3 Hurricane";
        }
        else if(strength < 157)
        {
            category = 4;
            rend.sprite = c4;
            rend.size = new Vector2(60, 60);
            GameObject.Find("Hurricane").GetComponent<TextMeshProUGUI>().text = "Cat 4 Hurricane";
        }
        else
        {
            category = 5;
            rend.sprite = c5;
            rend.size = new Vector2(60, 60);
            GameObject.Find("Hurricane").GetComponent<TextMeshProUGUI>().text = "Cat 5 Hurricane";
        }
        GameObject.Find("Wind Speed").GetComponent<TextMeshProUGUI>().text = "Wind Speed: " + (int)Mathf.Round(strength) + " mph";

        if((x > 672 || x < -960 || y > 540 || y < -540) && !dead)
        {
            dead = true;
            StartCoroutine(gc.failLevel());
        }
        //Debug.Log(rb.velocity.magnitude);



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "target" && category >= gc.cats[gc.level-1])
        {
            Sprite grayStar = Resources.Load("gstar", typeof(Sprite)) as Sprite;
            other.gameObject.GetComponent<SpriteRenderer>().sprite = grayStar;
            //Debug.Log("Level Complete");
            StartCoroutine(gc.passLevel());
        }
        
    }
}
