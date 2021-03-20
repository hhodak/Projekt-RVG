using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razina : MonoBehaviour
{

    public int razina = 1;
    public GameObject pozadina;
    public GameObject pozadinaPustinja;
    public GameObject pozadinaPustinjaChild;
    public GameObject bodoviTekst;
    public GameObject zivotiTekst;
    public GameObject tvoracNeprijatelja;
    public GameObject igrac;
    public float boja = 1.0f;
    public float kolicinaBoje = 0.05f;
    public float sekunde = 0.05f;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(razina)
        {
            case 1:
                if(tvoracNeprijatelja.GetComponent<StvaranjeNeprijatelja>().bossFight==true)
                {
                    if(tvoracNeprijatelja.GetComponent<StvaranjeNeprijatelja>().boss1GO && !igrac.activeSelf)
                    {
                        Poraz();
                    }
                    if (!tvoracNeprijatelja.GetComponent<StvaranjeNeprijatelja>().boss1GO && igrac.activeSelf)
                    {
                        Pobjeda();
                    }
                }
                break;
            default: break;
        }
    }

    public IEnumerator Zacrni()
    {
        if (boja > 0)
        {
            while (boja > 0)
            {
                boja -= kolicinaBoje;
                if (boja <= 0)
                    boja = 0;
                pozadinaPustinja.GetComponent<SpriteRenderer>().color = new Color(boja, boja, boja);
                pozadinaPustinjaChild.GetComponent<SpriteRenderer>().color = new Color(boja, boja, boja);
                yield return new WaitForSeconds(sekunde);
            }
        }
        yield return null;
    }

    IEnumerator VratiBoju()
    {
        if (boja < 1)
        {
            while (boja < 1)
            {
                pozadinaPustinja.GetComponent<SpriteRenderer>().color = new Color(boja, boja, boja);
                pozadinaPustinjaChild.GetComponent<SpriteRenderer>().color = new Color(boja, boja, boja);
                boja += kolicinaBoje;
                yield return new WaitForSeconds(sekunde);
            }
        }
        yield return null;
    }

    void Poraz()
    {
        Debug.Log("You lose!");
    }

    void Pobjeda()
    {
        Debug.Log("You win!");
        razina++;
        Debug.Log(razina);
    }
}
