using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{

    public Toggle toggleCijeliZaslon;
    public Dropdown dropdownRezolucija;
    public InputField inputImeIgraca;

    public Resolution[] rezolucija;
    public GameSettings gameSettings;
    public Button gumbSpremi;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        toggleCijeliZaslon.onValueChanged.AddListener(delegate { PromijeniZaslon(); });
        dropdownRezolucija.onValueChanged.AddListener(delegate { PromijeniRezoluciju(); });
        inputImeIgraca.onValueChanged.AddListener(delegate { PromijeniImeIgraca(); });
        gumbSpremi.onClick.AddListener(delegate { KlikSpremiPotavke(); });

        rezolucija = Screen.resolutions;
        foreach (Resolution resolution in rezolucija)
        {
            dropdownRezolucija.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        UcitajPostavke();
    }

    public void PromijeniZaslon()
    {
        gameSettings.cijeliZaslon = Screen.fullScreen = toggleCijeliZaslon.isOn;
    }

    public void PromijeniRezoluciju()
    {
        Screen.SetResolution(rezolucija[dropdownRezolucija.value].width, rezolucija[dropdownRezolucija.value].height, Screen.fullScreen);
        gameSettings.rezolucija = dropdownRezolucija.value;
    }

    public void PromijeniImeIgraca()
    {
        gameSettings.imeIgraca = inputImeIgraca.text;
    }

    public void UcitajPostavke()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
            toggleCijeliZaslon.isOn = gameSettings.cijeliZaslon;
            dropdownRezolucija.value = gameSettings.rezolucija;
            inputImeIgraca.text = gameSettings.imeIgraca;

            Screen.fullScreen = gameSettings.cijeliZaslon;

            dropdownRezolucija.RefreshShownValue();
        }
        else
        {
            Screen.fullScreen = true;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
    }

    public void SpremiPostavke()
    {
        string podaciJson = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", podaciJson);
    }

    public void KlikSpremiPotavke()
    {
        SpremiPostavke();
    }

}
