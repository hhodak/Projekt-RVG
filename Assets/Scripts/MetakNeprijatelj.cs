using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetakNeprijatelj : MonoBehaviour
{

    float brzina;
    Vector2 smjer;
    bool spreman;

    private void Awake()
    {
        brzina = 5.0f;
        spreman = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spreman)
        {
            Vector2 pozicija = transform.position;

            pozicija += smjer * brzina * Time.deltaTime;
            transform.position = pozicija;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            if (transform.position.x < min.x || transform.position.y < min.y || transform.position.x > max.x || transform.position.y > max.y)
            {
                Destroy(gameObject);
            }
        }
    }

    public void UsmjeriMetak(Vector2 dir)
    {
        smjer = dir.normalized;
        spreman = true;
    }

    private void OnTriggerEnter2D(Collider2D kolizija)
    {
        if (kolizija.tag == "IgracTag")
        {
            Destroy(gameObject);
        }
    }
}
