using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPressureScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float x;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //DIRECTION CALCULATION
        float n = Mathf.Pow(0.2f, Time.deltaTime);
        Vector2 residual = rb.velocity * n;
        Vector2 original = new Vector2(0, 0);
        x = transform.position.x;
        y = transform.position.y;

        //Trade Winds
        float differential = y + 215;
        if (((-1 / 1200f) * Mathf.Pow(differential, 2) + 55) > 0)
        {
            original.x -= ((-1 / 1200f) * Mathf.Pow(differential, 2) + 55);
        }

        //Westerlies
        float differential2 = y - 293;
        if (((-1 / 1200f) * Mathf.Pow(differential2, 2) + 55) > 0)
        {
            original.x += ((-1 / 1200f) * Mathf.Pow(differential2, 2) + 55);
        }

        //Monsoon
        float diffx = x + 720;
        float diffy = y + 10;
        if (Mathf.Pow(diffx, 2) + Mathf.Pow(diffy, 2) < Mathf.Pow(260f, 2))
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
        foreach (GameObject high in highs)
        {
            float diffhx = x - high.transform.position.x;
            float diffhy = y - high.transform.position.y;
            Vector2 temporario = new Vector2(diffhy, -1 * diffhx);
            temporario.Normalize();
            if (Mathf.Sqrt(Mathf.Pow(diffhx, 2) + Mathf.Pow(diffhy, 2)) < 200)
            {
                temporario = temporario * 60;
            }
            else
            {
                float multiplier = ((560 - Mathf.Sqrt(Mathf.Pow(diffhx, 2) + Mathf.Pow(diffhy, 2))) * 1 / 6);
                if (multiplier > 0)
                {
                    temporario = temporario * multiplier;
                }
            }

            if (Mathf.Sqrt(Mathf.Pow(diffhx, 2) + Mathf.Pow(diffhy, 2)) < 100)
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
            if(!GameObject.ReferenceEquals(low, this.gameObject))
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
                    temporario += oppose.normalized * 1000 / oppose.magnitude;
                }

                original += temporario;
            }
        }

        //The Hurricane
        GameObject furacao = GameObject.Find("Furacao");
        float difffx = x - furacao.transform.position.x;
        float difffy = y - furacao.transform.position.y;
        Vector2 temporariof = new Vector2(-1 * difffy, difffx);
        temporariof.Normalize();
        if (Mathf.Sqrt(Mathf.Pow(difffx, 2) + Mathf.Pow(difffy, 2)) < 150)
        {
            temporariof = temporariof * 50;
        }
        else
        {
            float multiplier = ((560 - Mathf.Sqrt(Mathf.Pow(difffx, 2) + Mathf.Pow(difffy, 2))) * 1 / 6);
            if (multiplier > 0)
            {
                temporariof = temporariof * multiplier;
            }
        }

        if (Mathf.Sqrt(Mathf.Pow(difffx, 2) + Mathf.Pow(difffy, 2)) < 100)
        {
            Vector2 oppose = new Vector2(difffx, difffy);
            temporariof += oppose.normalized * 1000 / oppose.magnitude;
        }

        original += temporariof;


        original = original * (1 - n);
        rb.velocity = original/2f + residual;
        //Debug.Log(rb.velocity.magnitude);
    }
}
