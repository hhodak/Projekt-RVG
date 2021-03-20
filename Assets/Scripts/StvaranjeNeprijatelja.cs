using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StvaranjeNeprijatelja : MonoBehaviour
{
    public GameObject easy1GO;
    public GameObject easy2GO;
    public GameObject easy3GO;
    public GameObject easy4GO;
    public GameObject easy5GO;
    public GameObject medium1GO;
    public GameObject medium2GO;
    public GameObject medium3GO;
    public GameObject medium4GO;
    public GameObject hard1GO;
    public GameObject hard2GO;
    public GameObject hard3GO;
    public GameObject hard4GO;
    public GameObject rare1GO;
    public GameObject rare2GO;
    public GameObject rare3GO;
    public GameObject boss1GO;
    public GameObject MedicGO;
    public GameObject AmmoGO;
    GameObject bodoviTekst;

    float maxSpawnRateInSeconds = 1.0f;
    float minSpawnRateInSeconds = 0.1f;
    float difSpawnRateInSeconds = 0.1f;
    int nasumicniNeprijatelj;
    int brojNeprijatelja;
    int mediumSpawnRate = 10;
    int hardSpawnRate = 100;
    int rareSpawnRate = 333;
    int medicSpawnRate = 125;
    int ammoSpawnRate = 35;

    public bool bossFight = false;

    // Use this for initialization
    void Start()
    {
        bodoviTekst = GameObject.FindGameObjectWithTag("BodoviTag");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StvoriNeprijatelja()
    {
        brojNeprijatelja++;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (bodoviTekst.GetComponent<Bodovanje>().Bodovi >= 10)
        {
            bossFight = true;
            GameObject neprijatelj = (GameObject)Instantiate(boss1GO);
            neprijatelj.transform.position = new Vector2(8.0f, 0.0f);
        }
        else
        {
            GameObject neprijatelj = (GameObject)Instantiate(VratiNeprijatelja(brojNeprijatelja));
            neprijatelj.transform.position = new Vector2(max.x, Random.Range(min.y, max.y));
            ScheduleNextSpawn();
        }

        
    }

    void ScheduleNextSpawn()
    {
        float spawnInSeconds;

        if (maxSpawnRateInSeconds > 1.0f)
        {
            spawnInSeconds = Random.Range(1.0f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInSeconds = 1.0f;
        }

        Invoke("StvoriNeprijatelja", spawnInSeconds);
    }

    void PovecajTezinuIgre()
    {
        if (maxSpawnRateInSeconds > minSpawnRateInSeconds)
        {
            maxSpawnRateInSeconds -= difSpawnRateInSeconds;
        }

        if (maxSpawnRateInSeconds == minSpawnRateInSeconds)
        {
            CancelInvoke("PovecajTezinuIgre");
        }
    }

    GameObject VratiNeprijatelja(int broj)
    {
        if (broj % hardSpawnRate == 0)
        {
            nasumicniNeprijatelj = Random.Range(1, 4);
            switch (nasumicniNeprijatelj)
            {
                case 1: return hard1GO;
                case 2: return hard2GO;
                case 3: return hard3GO;
                case 4: return hard4GO;
            }
            return hard1GO;
        }
        else if (broj % medicSpawnRate == 0)
        {
            return MedicGO;
        }
        else if (broj % ammoSpawnRate == 0)
        {
            return AmmoGO;
        }
        else if (broj % mediumSpawnRate == 0)
        {
            nasumicniNeprijatelj = Random.Range(1, 4);
            switch (nasumicniNeprijatelj)
            {
                case 1: return medium1GO;
                case 2: return medium2GO;
                case 3: return medium3GO;
                case 4: return medium4GO;
            }
            return medium1GO;
        }
        else if (broj % rareSpawnRate == 0)
        {
            nasumicniNeprijatelj = Random.Range(1, 3);
            switch (nasumicniNeprijatelj)
            {
                case 1: return rare1GO;
                case 2: return rare2GO;
                case 3: return rare3GO;
            }
            return rare1GO;
        }
        else
        {
            nasumicniNeprijatelj = Random.Range(1, 5);
            switch (nasumicniNeprijatelj)
            {
                case 1: return easy1GO;
                case 2: return easy2GO;
                case 3: return easy3GO;
                case 4: return easy4GO;
                case 5: return easy5GO;
            }
            return easy1GO;
        }
    }

    public void ScheduleEnemySpawner()
    {
        brojNeprijatelja = 0;
        Invoke("StvoriNeprijatelja", maxSpawnRateInSeconds);

        //povecanje tezine igre
        InvokeRepeating("PovecajTezinuIgre", 0.0f, 60.0f);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("StvoriNeprijatelja");
        CancelInvoke("PovecajTezinuIgre");
    }


}
