using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pozadina : MonoBehaviour
{
    public bool aktivna;
    public float brzina;
    Vector3 pocetnaPozicija;

    // Use this for initialization
    void Start()
    {
        aktivna = false;
        pocetnaPozicija = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (aktivna)
        {
            transform.Translate(new Vector3(-1, 0, 0) * brzina * Time.deltaTime);

            if (transform.position.x < -21.838)
            {
                transform.position = pocetnaPozicija;
            }
        }
    }

    public void ResetirajPoziciju()
    {
        transform.position = pocetnaPozicija;
    }
}
