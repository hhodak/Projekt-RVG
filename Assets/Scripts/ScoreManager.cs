using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScoreManager : MonoBehaviour
{

    string bodovi;
    string igrac;
    public GameObject ostvareniBodovi;
    public GameObject trenutniIgrac;
    public GameObject hsIgrac;
    public GameObject hsBodovi;

    public Scoresettings scoreSettings;

    public void UcitajRezultate()
    {
        if (File.Exists(Application.persistentDataPath + "/highscores.json"))
        {
            scoreSettings = JsonUtility.FromJson<Scoresettings>(File.ReadAllText(Application.persistentDataPath + "/highscores.json"));

            bodovi = scoreSettings.bodovi;
            igrac = scoreSettings.igrac;
        }
        else
        {
            scoreSettings = new Scoresettings();
            igrac = "";
            bodovi = "0";
            scoreSettings.bodovi = bodovi.ToString();
            scoreSettings.igrac = igrac.ToString();
            string podaciJson = JsonUtility.ToJson(scoreSettings, true);
            File.WriteAllText(Application.persistentDataPath + "/highscores.json", podaciJson);
        }
    }

    public void SpremiRezultate()
    {
        string podaciJson = JsonUtility.ToJson(scoreSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", podaciJson);
    }

    public void PozicionirajRezultat()
    {
        if (File.Exists(Application.persistentDataPath + "/highscores.json"))
        {
            if (int.Parse(ostvareniBodovi.GetComponent<Text>().text) > int.Parse(bodovi.ToString()))
            {
                bodovi = ostvareniBodovi.GetComponent<Text>().text;
                igrac = trenutniIgrac.GetComponent<Text>().text;
                scoreSettings.bodovi = bodovi.ToString();
                scoreSettings.igrac = igrac.ToString();
            }
        }
    }

    public void PrikaziRezultat()
    {
        if (File.Exists(Application.persistentDataPath + "/highscores.json"))
        {
            scoreSettings = JsonUtility.FromJson<Scoresettings>(File.ReadAllText(Application.persistentDataPath + "/highscores.json"));

            bodovi = scoreSettings.bodovi;
            igrac = scoreSettings.igrac;

            hsIgrac.GetComponent<Text>().text = igrac;
            hsBodovi.GetComponent<Text>().text = bodovi;
        }
    }

}
