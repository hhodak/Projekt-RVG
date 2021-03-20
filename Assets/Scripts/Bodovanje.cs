using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bodovanje : MonoBehaviour
{

    Text bodoviTekst;
    int bodovi;

    public int Bodovi
    {
        get
        {
            return this.bodovi;
        }
        set
        {
            this.bodovi = value;
            AzurirajBodove();
        }
    }

    // Use this for initialization
    void Start()
    {
        bodoviTekst = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AzurirajBodove()
    {
        if (bodovi < 0)
        {
            bodovi = 0;
        }
        string bodoviString = bodovi.ToString();
        bodoviTekst.text = bodoviString;
    }

}
