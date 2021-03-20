using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Municija : MonoBehaviour
{
    Text metciTekst;
    int metci;

    public int Metci
    {
        get
        {
            return this.metci;
        }
        set
        {
            this.metci = value;
            AzurirajMetke();
        }
    }

    // Use this for initialization
    void Start()
    {
        metciTekst = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AzurirajMetke()
    {
        if (metci < 0)
        {
            metci = 0;
        }
        string metciString = metci.ToString();
        metciTekst.text = metciString;
    }

}
