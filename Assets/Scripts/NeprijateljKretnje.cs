using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NeprijateljKretnje : MonoBehaviour
{
    GameObject bodoviTekstGO;
    GameObject metciTekstGO;
    GameObject zivotiTekstGO;

    public GameObject eksplozijaGO;

    public float brzina = 6.0f;
    public int zivot;
    public int bodoviUnistenje;
    public int bodoviProlaz;

    // Use this for initialization
    void Start()
    {
        bodoviTekstGO = GameObject.FindGameObjectWithTag("BodoviTag");
        metciTekstGO = GameObject.FindGameObjectWithTag("MunicijaTag");
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

        pozicija = new Vector2(pozicija.x - brzina * Time.deltaTime, pozicija.y);

        transform.position = pozicija;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.x < min.x)
        {
            if (transform.tag == "AmmoTag")
            {
                metciTekstGO.GetComponent<Municija>().Metci += 100;
                metciTekstGO.GetComponent<AudioSource>().Play();
            }
            if (transform.tag == "MedicTag")
            {
                zivotiTekstGO.GetComponent<Zivoti>().Zivot++;
                zivotiTekstGO.GetComponent<AudioSource>().Play();
            }
            bodoviTekstGO.GetComponent<Bodovanje>().Bodovi += bodoviProlaz;
            Destroy(gameObject);
        }
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
        if (transform.name == "rare1GO(Clone)" || transform.name == "rare2GO(Clone)" || transform.name == "rare3GO(Clone)")
        {
            NasumicniBonus();
        }
    }

    void Eksplodiraj()
    {
        GameObject eksplozija = (GameObject)Instantiate(eksplozijaGO);
        eksplozija.transform.position = transform.position;
    }

    void NasumicniBonus()
    {
        int tip = Random.Range(1, 3);
        switch (tip)
        {
            case 1: zivotiTekstGO.GetComponent<Zivoti>().Zivot++; break;
            case 2: metciTekstGO.GetComponent<Municija>().Metci += Random.Range(25, 50); break;
            case 3: bodoviTekstGO.GetComponent<Bodovanje>().Bodovi += Random.Range(10, 25); break;
            default: bodoviTekstGO.GetComponent<Bodovanje>().Bodovi += 10; break;
        }
    }
}
