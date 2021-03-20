using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metak : MonoBehaviour
{

    float brzina;

    // Use this for initialization
    void Start()
    {
        brzina = 8.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pozicija = transform.position;

        pozicija = new Vector2(pozicija.x + brzina * Time.deltaTime, pozicija.y);

        transform.position = pozicija;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.x > max.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D kolizija)
    {
        if (kolizija.tag == "NeprijateljTag" || kolizija.tag == "MedicTag" || kolizija.tag == "AmmoTag")
        {
            Destroy(gameObject);
        }
    }
}
