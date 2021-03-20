using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaktorNeprijatelj : MonoBehaviour
{

    public GameObject metakNeprijatelj;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(IspaliMetak());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IspaliMetak()
    {
        GameObject igrac = GameObject.Find("IgracGO");

        while (gameObject)
        {

            if (igrac != null)
            {
                GameObject metak = (GameObject)Instantiate(metakNeprijatelj);
                metak.transform.position = transform.position;
                GetComponent<AudioSource>().Play();

                Vector2 smjer = igrac.transform.position - metak.transform.position;

                metak.GetComponent<MetakNeprijatelj>().UsmjeriMetak(smjer);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
