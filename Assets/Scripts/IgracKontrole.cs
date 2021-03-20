using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IgracKontrole : MonoBehaviour
{
    GameObject metciTekstGO;
    GameObject zivotiTekstGO;

    public GameObject GameManagerGO;
    public GameObject MetakGO;
    public GameObject Reaktor;
    public GameObject eksplozijaGO;
    public float brzina = 5.0f;
    public int brojZivota;
    public int brojMetaka;

    // Use this for initialization
    void Start()
    {
        zivotiTekstGO = GameObject.FindGameObjectWithTag("ZivotiTag");
        metciTekstGO = GameObject.FindGameObjectWithTag("MunicijaTag");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (metciTekstGO.GetComponent<Municija>().Metci > 0)
            {
                GetComponent<AudioSource>().Play();
                GameObject metak = (GameObject)Instantiate(MetakGO);
                metak.transform.position = Reaktor.transform.position;
                metciTekstGO.GetComponent<Municija>().Metci--;
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 smjer = new Vector2(x, y).normalized;

        Pokret(smjer);
    }

    void Pokret(Vector2 smjer)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //dolje-lijevo
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //gore-desno

        max.x = max.x - 0.3f;
        min.x = min.x + 0.3f;

        max.y = max.y - 0.3f;
        min.y = min.y + 0.3f;

        Vector2 pozicija = transform.position;

        pozicija += smjer * brzina * Time.deltaTime;

        pozicija.x = Mathf.Clamp(pozicija.x, min.x, max.x);
        pozicija.y = Mathf.Clamp(pozicija.y, min.y, max.y);

        transform.position = pozicija;
    }

    private void OnTriggerEnter2D(Collider2D kolizija)
    {
        if (kolizija.tag == "NeprijateljTag" || kolizija.tag == "AmmoTag" || kolizija.tag == "MedicTag" || kolizija.tag=="BossTag" || kolizija.tag=="MetakNeprijateljTag")
        {
            Eksplodiraj();

            zivotiTekstGO.GetComponent<Zivoti>().Zivot--;

            if (zivotiTekstGO.GetComponent<Zivoti>().Zivot == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }
    }

    void Eksplodiraj()
    {
        GameObject eksplozija = (GameObject)Instantiate(eksplozijaGO);
        eksplozija.transform.position = transform.position;
    }

    public void PostaviZivote()
    {
        gameObject.SetActive(true);
        transform.position = new Vector2(-10, 0);
    }
}
