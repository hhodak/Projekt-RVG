using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gumbIgraj;
    public GameObject gumbPostavke;
    public GameObject gumbIzlaz;
    public GameObject igrac;
    public GameObject enemySpawner;
    public GameObject labelaKrajIgre;
    public GameObject bodoviTekstGO;
    public GameObject metciTekstGO;
    public GameObject zivotiTekstGO;
    public GameObject postavke;
    public GameObject scoreManager;
    public GameObject pozadina;
    public GameObject razina;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    public GameManagerState GMState;

    // Use this for initialization
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                razina.GetComponent<Razina>().pozadina.SetActive(true);
                labelaKrajIgre.SetActive(false);
                gumbIgraj.SetActive(true);
                gumbPostavke.SetActive(true);
                gumbIzlaz.SetActive(true);
                scoreManager.GetComponent<ScoreManager>().PrikaziRezultat();
                break;

            case GameManagerState.Gameplay:
                pozadina.GetComponent<Pozadina>().aktivna = true;
                razina.GetComponent<Razina>().pozadina.SetActive(false);

                //StartCoroutine(razina.GetComponent<Razina>().Zacrni());
                razina.GetComponent<Razina>().razina++;
                int brojMetaka = igrac.GetComponent<IgracKontrole>().brojMetaka;
                int brojZivota = igrac.GetComponent<IgracKontrole>().brojZivota;

                bodoviTekstGO.GetComponent<Bodovanje>().Bodovi = 0;
                zivotiTekstGO.GetComponent<Zivoti>().Zivot = brojZivota;
                metciTekstGO.GetComponent<Municija>().Metci = brojMetaka;

                gumbIgraj.SetActive(false);
                gumbPostavke.SetActive(false);
                gumbIzlaz.SetActive(false);
                igrac.GetComponent<IgracKontrole>().PostaviZivote();
                enemySpawner.GetComponent<StvaranjeNeprijatelja>().ScheduleEnemySpawner();
                break;

            case GameManagerState.GameOver:
                razina.GetComponent<Razina>().razina = 0;
                pozadina.GetComponent<Pozadina>().aktivna = false;
                pozadina.GetComponent<Pozadina>().ResetirajPoziciju();
                enemySpawner.GetComponent<StvaranjeNeprijatelja>().UnscheduleEnemySpawner();
                scoreManager.GetComponent<ScoreManager>().UcitajRezultate();
                scoreManager.GetComponent<ScoreManager>().PozicionirajRezultat();
                scoreManager.GetComponent<ScoreManager>().SpremiRezultate();

                labelaKrajIgre.SetActive(true);
                labelaKrajIgre.GetComponent<AudioSource>().PlayDelayed(2);

                Invoke("ChangeToOpeningState", 6.0f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState stanje)
    {
        GMState = stanje;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    public void OtvoriPostavke()
    {
        gumbIgraj.SetActive(false);
        gumbPostavke.SetActive(false);
        gumbIzlaz.SetActive(false);
        postavke.SetActive(true);
    }

    public void ZatvoriPostavke()
    {
        gumbIgraj.SetActive(true);
        gumbPostavke.SetActive(true);
        gumbIzlaz.SetActive(true);
        postavke.SetActive(false);
    }

    public void ZatvoriIgru()
    {
        Application.Quit();
    }

}
