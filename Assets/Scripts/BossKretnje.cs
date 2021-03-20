using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossKretnje : MonoBehaviour
{
    GameObject bodoviTekstGO;
    GameObject zivotiTekstGO;
    public GameObject eksplozijaGO;

    public float brzina = 3.0f;
    public int zivot = 10;
    public int bodoviUnistenje = 100;

    Vector3 odredisnaPozicija;
    bool noviCiklus = true;

    //public bool stanjeIgrac = true;
    //public bool stanjeBoss = false;

    // Use this for initialization
    void Start()
    {
        bodoviTekstGO = GameObject.FindGameObjectWithTag("BodoviTag");
        zivotiTekstGO = GameObject.FindGameObjectWithTag("ZivotiTag");
    }

    // Update is called once per frame
    void Update()
    {
        if (zivotiTekstGO.GetComponent<Zivoti>().Zivot == 0)
        {
            Destroy(gameObject);
        }

        Vector2 pozicija = transform.position;

        if (pozicija.x <= 5)
        {
            brzina = 0;
            Invoke("KretnjaVertikalno", 1.0f);
        }
        else
        {
            KretnjaHorizontalno();
        }
    }

    void KretnjaHorizontalno()
    {
        Vector2 pozicija = transform.position;
        pozicija = new Vector2(pozicija.x - brzina * Time.deltaTime, pozicija.y);
        transform.position = pozicija;
    }
    void KretnjaVertikalno()
    {
        brzina = 5.0f;
        if (noviCiklus)
        {
            float noviX = 5.0f;
            float noviY = Random.Range(-3.0f, 3.0f);
            odredisnaPozicija = new Vector3(noviX, noviY);
        }

        transform.position = Vector3.MoveTowards(transform.position, odredisnaPozicija, brzina * Time.deltaTime);
        if (transform.position.y != odredisnaPozicija.y)
            noviCiklus = false;
        else
            noviCiklus = true;

    }
    private void OnTriggerEnter2D(Collider2D kolizija)
    {
        if (kolizija.tag == "IgracTag" || kolizija.tag == "MetakTag")
        {
            zivot--;
            if (zivot == 0)
            {
                Eksplodiraj();
                bodoviTekstGO.GetComponent<Bodovanje>().Bodovi += bodoviUnistenje;
                Destroy(gameObject);
            }
        }
    }

    void Eksplodiraj()
    {
        GameObject eksplozija = (GameObject)Instantiate(eksplozijaGO);
        eksplozija.transform.position = transform.position;
    }
}
