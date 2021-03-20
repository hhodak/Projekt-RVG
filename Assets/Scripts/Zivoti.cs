using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Zivoti : MonoBehaviour
{

    Text zivotiTekst;
    int zivoti;

    public int Zivot
    {
        get
        {
            return this.zivoti;
        }
        set
        {
            this.zivoti = value;
            AzurirajZivote();
        }
    }

    // Use this for initialization
    void Start()
    {
        zivotiTekst = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AzurirajZivote()
    {
        if (zivoti < 0)
        {
            zivoti = 0;
        }
        string zivotiString = zivoti.ToString();
        zivotiTekst.text = zivotiString;
    }

}
